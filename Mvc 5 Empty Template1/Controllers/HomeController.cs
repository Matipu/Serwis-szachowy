using System.Web.Mvc;
using System.Web.Script.Serialization;
using Chess;
using WebApplication1.src.Response;
using System.Collections.Generic;
using SerwisSzachowy.Models.Repository;
using SerwisSzachowy.Models;
using SerwisSzachowy.src.Chess.Serializers;
using System;
using Mvc_5_Empty_Template1.Models;
using Mvc_5_Empty_Template1.src;
using WebApplication1.src.Chess.Figures;

namespace SerwisSzachowy.Controllers
{
    public class ChessController : Controller
    {
        UserRepository userRepository = new UserRepository();
        GameRepository gameRepository = new GameRepository();
        JavaScriptSerializer js = new JavaScriptSerializer();
        ChessboardSerializer chessboardSerializer = new ChessboardSerializer();
        User user;
        Dictionary<int, Game> games;
        public ActionResult initController()
        {
            if (Session["user"] == null)
            {
                Response.StatusCode = 200;
                return Content(js.Serialize(new { error = "Nie jesteś zalogowany!", redirect = "login" }).ToString(), "application/json");
            }

            games = (Dictionary<int, Game>)Session["games"];
            this.user = (User)Session["user"];

            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return null;
        }

        // GET: Home
        public ActionResult Index(int gameId)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;
            GameResponse gameResponse = gameRepository.getItem(user.Id, gameId);

            this.Response.StatusCode = 200;
            return Content(js.Serialize(gameResponse).ToString(), "application/json");
        }

        public ActionResult allGames()
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;

            List<GameResponse> response = gameRepository.getAll(user.Id);
            games = (Dictionary<int, Game>)Session["games"];
            if (((Dictionary<int, Game>)Session["games"]).Count == 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    games.Add(response[i].id, chessboardSerializer.toGame(response[i]));
                }
                Session["games"] = games;
            }

            this.Response.StatusCode = 200;
            return Content(js.Serialize(response).ToString(), "application/json");
        }

        // 
        // GET: /PossibleMoves/{id}
        public ActionResult PossibleMoves(int gameId, int line, int collumn)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;

            Game game = games[gameId];
            if (game.chessboard.figures[line][collumn] == null)
            {
                return Content(js.Serialize(new { status = "Wrong figure" }), "application/json");
            }

            if (game.numberOfMovements % 2 == 0 && game.chessboard.figures[line][collumn].color != "w")
            {
                return Content(js.Serialize(new { status = "Wrong color" }), "application/json");
            }

            if (game.numberOfMovements % 2 == 1 && game.chessboard.figures[line][collumn].color != "b")
            {
                return Content(js.Serialize(new { status = "Wrong color" }), "application/json");
            }

            if (game.chessboard.posibleMoves[line][collumn] == null && game.chessboard.getFigure(line, collumn) != null)
            {
                game.chessboard.posibleMoves[line][collumn] = new List<Coordinate>();
                game.chessboard.posibleMoves[line][collumn] = game.chessboard.getAllPossibleMoves(line, collumn);
            }

            List<Chess.Coordinate> possiblesMoves = game.chessboard.posibleMoves[line][collumn];
            String color = game.chessboard.figures[line][collumn].color;
            Boolean isPossibleMove;
            for (int k = 0; k < possiblesMoves.Count; k++)
            {
                isPossibleMove = true;
                Figure zbitaFigura = game.chessboard.figures[possiblesMoves[k].line][possiblesMoves[k].collumn];
                game.chessboard.makeAMove(line, collumn, possiblesMoves[k].line, possiblesMoves[k].collumn);
                if (ChessboardAnalizer.isCheck(game.chessboard, color))
                {
                    isPossibleMove = false;
                }
                game.chessboard.makeAMove(possiblesMoves[k].line, possiblesMoves[k].collumn, line, collumn);
                if (zbitaFigura != null)
                {
                    game.chessboard.addFigure(zbitaFigura);
                }
                //game.chessboard.figures[possiblesMoves[k].line][possiblesMoves[k].collumn] = zbitaFigura;
                if (isPossibleMove == false)
                {
                    possiblesMoves.Remove(possiblesMoves[k]);
                    k--;
                }
            }


            this.Response.StatusCode = 200;
            return Content(js.Serialize(possiblesMoves).ToString(), "application/json");
        }

        // 
        // GET: /makeAMove
        public ActionResult makeAMove(int gameId, int iLine, int iCollumn, int tLine, int tCollumn)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;


            Game game = games[gameId];
            if (game.chessboard.figures[iLine][iCollumn] != null)//color
            {
                if (game.numberOfMovements % 2 == 0 && game.chessboard.figures[iLine][iCollumn].color != "w")
                {
                    return Content(js.Serialize(new { status = "Wrong color" }), "application/json");
                }
                if (game.numberOfMovements % 2 == 1 && game.chessboard.figures[iLine][iCollumn].color != "b")
                {
                    return Content(js.Serialize(new { status = "Wrong color" }), "application/json");
                }

            }
            Move move = new Move();
            move.init = new Coordinate(iLine, iCollumn);
            move.target = new Coordinate(tLine, tCollumn);
            if (game.chessboard.figures[tLine][tCollumn] != null)
            {
                move.compactFigure = game.chessboard.figures[tLine][tCollumn].getName();
            }
            else
            {
                move.compactFigure = null;
            }

            game.numberOfMovements++;
            game.chessboard.makeAMove(iLine, iCollumn, tLine, tCollumn);
            game.history.Add(move);

            game.id = gameId;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ((Dictionary<int, Game>)Session["games"])[game.id].chessboard.posibleMoves[i][j] = null;
                }
            }
            game.finishStatus = ChessboardAnalizer.calculateFinishStatus(game.chessboard);
            gameRepository.save(user.Id, gameId, chessboardSerializer.toGameResponse(game));
            

            SI si = new Mvc_5_Empty_Template1.src.SI();
            si.makeASIMove(game, user.Id);


            
            

            this.Response.StatusCode = 200;
            return Content(js.Serialize(new { status = "OK" }), "application/json");
        }


        // GET: /PossibleMoves/
        public ActionResult AllPossibleMoves()
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;
            List<List<Coordinate>> allCoordinates = new List<List<Coordinate>>();
            Chessboard chessboard = new Chessboard();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    allCoordinates.Add(chessboard.getAllPossibleMoves(i, j));
                }
            }

            this.Response.StatusCode = 200;
            return Content(js.Serialize(allCoordinates).ToString(), "application/json");

        }

        // 
        // GET /login/{email}/{password}
        public ActionResult login(String email, String password)
        {
            Session.RemoveAll();
            User user = userRepository.get(email, password);

            Session.Add("user", user);
            Session["user"] = user;
            Session["games"] = new Dictionary<int, Game>();
            Session.Timeout = 9000;
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            this.Response.StatusCode = 200;
            return Content(js.Serialize(new { sessionId = Session.SessionID, user = user }).ToString(), "application/json");
        }

        // 
        // GET /AddNewGameWitchComputer
        public ActionResult AddNewGameWitchComputer(String color, String difficult)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;
            if (color != null && difficult != null)
            {
                userRepository.addStartedGames(user);
                Game game = new Game();
                game.chessboard = new Chessboard();
                game.difficult = difficult;
                game.playerColor = color;
                game.startDate = new DateTime();
                game.id = user.startedGames;
                game.numberOfMovements = 0;
                if (color == "b")
                {
                    game.idBlackPlayer = user.Id;
                }
                else
                {
                    game.idWhitePlayer = user.Id;
                }
                ((Dictionary<int, Game>)Session["games"])[game.id] = game;
                gameRepository.save(user.Id, user.startedGames, chessboardSerializer.toGameResponse(game));

            }

            this.Response.StatusCode = 200;
            return Content("OK", "application/json");
        }

        // 
        // GET /getUserSettings
        public ActionResult getUserSettings()
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;

            return Content(js.Serialize(new { background_color = this.user.background_color, background_color2 = this.user.background_color2 }).ToString(), "application/json");
        }

        // 
        // GET /setUserSettings
        public ActionResult setUserSettings(string color1, string color2)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;

            userRepository.setColors(user, color1, color2);

            return Content(js.Serialize(new { status = "OK" }), "application/json");
        }

        // 
        // GET /setUserSettings
        public ActionResult backMove(int gameId)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;


            Game game = games[gameId];

            for(int i = 0; i < 2; i++)
            {
                Move lastMove = game.history[game.history.Count - 1];
                game.numberOfMovements--;
                game.chessboard.makeAMove(lastMove.target.line, lastMove.target.collumn, lastMove.init.line, lastMove.init.collumn);
                Figure compactFigure = chessboardSerializer.getFigureByName(lastMove.compactFigure, lastMove.target.line, lastMove.target.collumn);
                if (compactFigure != null)
                {
                    game.chessboard.addFigure(compactFigure);
                }
                game.finishStatus = null;
                game.history.Remove(lastMove);
            }


            game.id = gameId;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ((Dictionary<int, Game>)Session["games"])[game.id].chessboard.posibleMoves[i][j] = null;
                }
            }
            gameRepository.save(user.Id, gameId, chessboardSerializer.toGameResponse(game));



            this.Response.StatusCode = 200;
            return Content(js.Serialize(new { status = "OK" }), "application/json");
        }

        // 
        // GET /przewaga
        public ActionResult advantage(int gameId)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;
            Game game = games[gameId];
            float advantage = ChessboardAnalizer.getWhiteAdvantage(game.chessboard);
            return Content(js.Serialize(new { advantage = advantage }).ToString(), "application/json");
        }
    }
}
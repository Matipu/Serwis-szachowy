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
            if(Session["user"] == null)
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
            if (game.chessboard.posibleMoves[line][collumn] == null && game.chessboard.getFigure(line,collumn) != null)
            {
                game.chessboard.posibleMoves[line][collumn] = new List<Coordinate>();
                game.chessboard.posibleMoves[line][collumn] = game.chessboard.getAllPossibleMoves(line, collumn);
            }
            this.Response.StatusCode = 200;
            return Content(js.Serialize(game.chessboard.posibleMoves[line][collumn]).ToString(), "application/json");
        }

        // 
        // GET: /makeAMove
        public ActionResult makeAMove(int gameId, int iLine, int iCollumn, int tLine, int tCollumn)
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;


            Game game = games[gameId];
            Move move = new Move();
            move.init = new Coordinate(iLine, iCollumn);
            move.target = new Coordinate(tLine, tCollumn);
            if (game.chessboard.figures[tLine][tCollumn] != null)
            {
                move.compactFigure = game.chessboard.figures[tLine][tCollumn].getName();
            }else
            {
                move.compactFigure = null;
            }
            

            game.chessboard.figures[tLine][tCollumn] = game.chessboard.figures[iLine][iCollumn];
            game.chessboard.figures[tLine][tCollumn].position.collumn = tCollumn;
            game.chessboard.figures[tLine][tCollumn].position.line = tLine;
            game.chessboard.figures[iLine][iCollumn] = null;
            game.history.Add(move);
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


        // GET: /PossibleMoves/
        public ActionResult AllPossibleMoves()
        {
            ActionResult result;
            if ((result = initController()) != null)
                return result;
            List<List<Coordinate>> allCoordinates = new List<List<Coordinate>>();
            Chessboard chessboard = new Chessboard();
            for (int i = 0; i < 8; i++){
                for (int j = 0; j < 8; j++){
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
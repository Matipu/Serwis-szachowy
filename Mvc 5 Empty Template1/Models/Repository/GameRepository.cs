using Chess;
using Newtonsoft.Json;
using SerwisSzachowy.src.Chess.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebApplication1.src.Response;

namespace SerwisSzachowy.Models.Repository
{
    public class GameRepository
    {
        ChessboardSerializer chessboardSerializer = new ChessboardSerializer();
        protected string getFileName(int userId, int gameId)
        {
            return getUserGamesDirectory(userId) + "\\" + gameId.ToString() + ".json";
        }

        protected string getUserGamesDirectory(int userId)
        {
            return @"C:\Data\Games\" + userId.ToString();
        }

        public GameResponse getItem(string file)
        {
            string json = System.IO.File.ReadAllText(file);
            GameResponse gameResponse = JsonConvert.DeserializeObject<GameResponse>(json);

            return gameResponse;
        }
        public GameResponse getItem(int userId, int gameId)
        {
            return getItem(getFileName(userId, gameId));
        }

        public void save(int userId, int gameId, GameResponse gameResponse)
        {
            System.IO.File.WriteAllText(getFileName(userId, gameId), JsonConvert.SerializeObject(gameResponse));
        }

        public List<GameResponse> getAll(int userId)
        {
            List<GameResponse> response = new List<GameResponse>();
            string[] fileEntries = Directory.GetFiles(getUserGamesDirectory(userId));
            foreach (string fileName in fileEntries)
            {
                response.Add(getItem(fileName));
            }
            return response;
        }


        public long generateNewGameId(int userId)
        {
            long response;
            string[] fileEntries = Directory.GetFiles(getUserGamesDirectory(userId));
            foreach (string fileName in fileEntries)
            {
                //getItem(fileName).id;
            }
            return 5;
        }

    }
}
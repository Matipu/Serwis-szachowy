using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_5_Empty_Template1.Models;

namespace SerwisSzachowy.Models.Repository
{
    public class UserRepository
    {
        protected Database1Entities ctx;

        public UserRepository()
        {
            this.ctx = new Database1Entities();
        }
        public List<User> addUser(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return null;
        }
        public User get(String email, string password)
        {
            return ctx.Users.FirstOrDefault(user => (user.Email == email && user.Password == password));
        }
        public User get(long id)
        {
            return ctx.Users.FirstOrDefault(user => (user.Id == id));
        }
        public void addStartedGames(User tUser)
        {
            ctx.Users.Remove(ctx.Users.FirstOrDefault(user => (user.Id == tUser.Id)));
            tUser.startedGames++;
            ctx.Users.Add(tUser);
            ctx.SaveChanges();
        }
        public void addFinishedGames(User tUser)
        {
            ctx.Users.Remove(ctx.Users.FirstOrDefault(user => (user.Id == tUser.Id)));
            tUser.finishedGames++;
            ctx.Users.Add(tUser);
            ctx.SaveChanges();
        }

        public void setColors(User tUser, String color1, String color2)
        {
            ctx.Users.Remove(ctx.Users.FirstOrDefault(user => (user.Id == tUser.Id)));
            tUser.background_color = color1;
            tUser.background_color2 = color2;
            ctx.Users.Add(tUser);
            ctx.SaveChanges();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using bright.Models;

namespace bright.Controllers
{
    public class HomeController : Controller
    {
        private BrightContext _context;
        public HomeController(BrightContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {

            return View("Index");
        }
        [HttpPost]
        [Route("addUser")]
        public IActionResult addUser(UserViewModel user)
        {
            if(ModelState.IsValid)
            {
                
                User newUser = new User
                {
                    name = user.name,
                    alias = user.alias,
                    email = user.email,
                    password = user.password
                };
                _context.Add(newUser);
                _context.SaveChanges();
                User checkUser = _context.users.SingleOrDefault(users => user.email == users.email);
                HttpContext.Session.SetInt32("userid", checkUser.userid);
                int? userid = HttpContext.Session.GetInt32("userid");
                return RedirectToAction("bright");
            }
            else
            {
                return View ("Index");
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult loginUser(LoginViewModel logged)
        {
            User verifyUser = _context.users.SingleOrDefault(users => users.email == logged.logEmail && users.password == logged.logPassword);
            if(ModelState.IsValid)
            {
                if(verifyUser == null)
                {
                    ViewBag.error = "Login information incorrect!";
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("userid", verifyUser.userid);
                    int? userid = HttpContext.Session.GetInt32("userid");
                    return RedirectToAction("bright");
                }
            }
            
            return View("Index");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
        [HttpGet]
        [Route("bright")]
        public IActionResult bright()
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid != null)
            {
            ViewBag.allPosts = _context.posts.Include(post => post.Likers).ThenInclude(u => u.users).ToList();
            ViewBag.userid = userid;
            User currentUser = _context.users.SingleOrDefault(user => user.userid == (int)userid);  
            ViewBag.active = currentUser;            
            return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("addPost")]
        public IActionResult addPost(BrightViewModel add)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid != null)
            {
            if(ModelState.IsValid)
            {
                Bright newPost = new Bright
                {
                    post = add.post,
                    likebyid = (int)userid
                };
                _context.Add(newPost);
                _context.SaveChanges();
                }
                return RedirectToAction("bright");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("create/like/{postId}")]
        public IActionResult LikePost(int postId)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid !=null)
            {
                Like newLike = new Like
                {
                    postsid = postId,
                    likersid = (int)userid
                };
                _context.Add(newLike);
                _context.SaveChanges();
                return RedirectToAction("bright");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("delete/like/{postId}")]
        public IActionResult DeleteLike(int postId)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid !=null)
            {
                Like removeLiker = _context.likes.SingleOrDefault(p => p.postsid == postId && p.likersid == (int)userid);
                _context.likes.Remove(removeLiker);
                _context.SaveChanges();
                return RedirectToAction("bright");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("delete/post/{postId}")]
        public IActionResult RemovePost(int postId)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid !=null)
            {
                Bright removePost = _context.posts.SingleOrDefault(p => p.postid == postId);
                var allLikers = _context.likes.Where(l => l.postsid == postId);
                foreach(var each in allLikers)
                {
                    _context.likes.Remove(each);

                }
                _context.posts.Remove(removePost);
                _context.SaveChanges();
                return RedirectToAction("bright");
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Route("detail/{postId}")]
        public IActionResult Detail(int postId)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            List<Bright> OnePost = _context.posts.Where(p => p.postid == postId).Include(p => p.Likers).ThenInclude(l => l.users).ToList();
            if(OnePost.Count == 1)
            {
                ViewBag.userid = userid;
                ViewBag.thisPost = OnePost[0];
                return View();
            }
            return RedirectToAction("bright");
        }
        [HttpGet]
        [Route("userpage/{userId}")]
        public IActionResult userpage(int userId)
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            List<User> OneUser = _context.users.Where( u => u.userid == userId)
            .Include(u => u.Liking)
            .ThenInclude(a => a.users)
            .ToList();
            if(OneUser.Count ==1)
            {
                ViewBag.userid = userid;
                ViewBag.thisUser = OneUser[0];
                return View();
            }
            return RedirectToAction("bright");
        }
    }
}

using System.Security.Cryptography.X509Certificates;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using SQLitePCL;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController: ControllerBase
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            this._context = context;
        }

        // GET api/posts
        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            return this._context.Posts.ToList();
        }
        ///<summary>
        ///Get api/post/[id]
        ///</summary>
        ///<param name="id">post id</param> 
        ///<returns> A single Post</return>
        ///
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Post> GetById(Guid id)
        {
            var post = this._context.Posts.Find(id);
            if(post is null)
            {
                return NotFound();

            }
        
            return Ok(post);
            ///<summary
            ///POST api/post
            
            ///<param name="request">JSON request containing  post field </param>
            /// <returns> A new post Field</returns>
            
            
            
            

        
        }

         [HttpPost(Name = "Create")]
        
            
        public ActionResult<Post>Create([FromBody] Post request)
        {
           var post = new Post
           {
            Id = request.Id,
            Title = request.Title,
            Body = request.Body,
            Date = request.Date
           };
           _context.Posts.Add(post);
           var success = _context.SaveChanges()>0;
           if(success)
           {
            return Ok(post);
            
           }
          throw new Exception("Error creating post");
///<summary>
///PUT api/post
///</summary>
///<param name="request">JSON request containing one more more updated post fields</param>
///

        
        
    }
    [HttpPut(Name ="Update")]
    public ActionResult<Post>Update([FromBody]Post request)
    {
        var post = _context.Posts.Find(request.Id);
        if(post == null)
        {
            throw new Exception("could not Find post");

        }
        post.Title = request.Title !=null ?request.Title: post.Title;
        post.Body = request.Body !=null? request.Body: post.Body;
        post.Date = request.Date !=DateTime.MinValue ?request.Date : post.Date;
        var success = _context.SaveChanges()>0;
        if(success)
        {
            return Ok(post);

        }
        throw new Exception("Error updating post");
    }
    
}

}
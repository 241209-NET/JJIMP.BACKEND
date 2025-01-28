using Microsoft.AspNetCore.Mvc;
using JJIMP.API.DTO;
using JJIMP.API.Service;

namespace JJIMP.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetComment(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentById(id);
            return Ok(comment);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateComment(CreateCommentDTO commentDTO)
    {
        try
        {
            var comment = await _commentService.CreateComment(commentDTO);
            return Ok(comment);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateComment(UpdateCommentDTO commentDTO)
    {
        try
        {
            var comment = await _commentService.UpdateComment(commentDTO);
            return Ok(comment);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        var comment = await _commentService.DeleteComment(id);
        return Ok(comment);
    }
}
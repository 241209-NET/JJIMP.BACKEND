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
        var comment = await _commentService.GetCommentById(id);
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult> CreateComment(CreateCommentDTO commentDTO)
    {
        var comment = await _commentService.CreateComment(commentDTO);
        return Ok(comment);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateComment(UpdateCommentDTO commentDTO)
    {
        var comment = await _commentService.UpdateComment(commentDTO);
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        var comment = await _commentService.DeleteComment(id);
        return Ok(comment);
    }
}
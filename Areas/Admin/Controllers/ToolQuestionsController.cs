using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class ToolQuestionsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ToolQuestionsController(ApplicationDbContext db){_db=db;}
    public async Task<IActionResult> Index()=>View(await _db.ToolQuestions.Include(x=>x.Tool).OrderBy(x=>x.ToolId).ThenBy(x=>x.SortOrder).ToListAsync());
    public async Task<IActionResult> Create(){await LoadTools();return View("Edit",new ToolQuestionEditViewModel());}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(ToolQuestionEditViewModel vm){if(!ModelState.IsValid){await LoadTools();return View("Edit",vm);}var q=new ToolQuestion();Map(vm,q);_db.Add(q);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var q=await _db.ToolQuestions.FindAsync(id);if(q==null)return NotFound();await LoadTools();return View(new ToolQuestionEditViewModel{Id=q.Id,ToolId=q.ToolId,Prompt=q.Prompt,HelpText=q.HelpText,SortOrder=q.SortOrder,MinScore=q.MinScore,MaxScore=q.MaxScore});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, ToolQuestionEditViewModel vm){var q=await _db.ToolQuestions.FindAsync(id);if(q==null)return NotFound();Map(vm,q);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var q=await _db.ToolQuestions.FindAsync(id);if(q!=null){_db.Remove(q);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
    async Task LoadTools()=>ViewBag.Tools=new SelectList(await _db.Tools.OrderBy(x=>x.Title).ToListAsync(),"Id","Title");
    static void Map(ToolQuestionEditViewModel vm, ToolQuestion q){q.ToolId=vm.ToolId;q.Prompt=vm.Prompt;q.HelpText=vm.HelpText;q.SortOrder=vm.SortOrder;q.MinScore=vm.MinScore;q.MaxScore=vm.MaxScore;q.UpdatedUtc=DateTime.UtcNow;}
}

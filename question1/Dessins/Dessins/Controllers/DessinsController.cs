using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }

        [HttpGet]
        public ActionResult GetDrawing2()
        {
            ChangeColor changecolor1 = new ChangeColor("blue");
            DrawCircle Circle = new DrawCircle(1, 1);
            Wait wait1 = (new Wait(3));
            ChangeColor changecolor2 = new ChangeColor("red");
            DrawSquare square1 = (new DrawSquare(0, 2));
            DrawSquare square2=(new DrawSquare(2, 2));
            Wait wait2=(new Wait(1));
            ChangeColor changecolor3 = new ChangeColor("yellow");
            DrawStar drawstar =(new DrawStar(1, 3, 20));
            changecolor1.DrawingEvents.Add(Circle);
            Circle.DrawingEvents.Add(wait1);
            wait1.DrawingEvents.Add(changecolor2);
            changecolor2.DrawingEvents.Add(square1);
            square1.DrawingEvents.Add(square2);
            square2.DrawingEvents.Add(wait2);
            wait2.DrawingEvents.Add(changecolor3);
            changecolor3.DrawingEvents.Add(drawstar);
            return Ok(changecolor1);
        }

        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé
    }
}

using JustChess.Board.Contracts;
using JustChess.Renderers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Renderers
{
    class MockConsoleRenderer : IRenderer
    {
        public void PrintErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }

        public void RenderBoard(IBoard board)
        {
            Console.WriteLine("MockConsoleRenderer::RenderBoard()");
        }

        public void RenderMainMenu()
        {
            Console.WriteLine("MockConsoleRenderer::RenderMainMenu()");
        }
    }
}

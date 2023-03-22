using System.Diagnostics;

namespace Shirahadori
{
    public class Record
    {
        public float actionTiming { set; get; }
        public float endTiming { set; get; }
        public bool pass { set; get; } = false;

        private Stopwatch stopwatch;

        public Record(GameManager gameManager)
        {
            stopwatch = new Stopwatch();            
            gameManager.OnAction += OnAction;
            gameManager.OnStartAction += OnStartAction;
            gameManager.OnEndAction += OnEndAction;
            gameManager.OnReset += OnReset;
        }

        private void OnStartAction()
        {
            stopwatch.Start();
        }

        private void OnAction()
        {
            pass = false;
            SetTiming();
        }        

        private void OnEndAction()
        {
            pass = true;
            SetTiming();
        }

        private void OnReset()
        {
            stopwatch.Reset();
        }

        private void SetTiming()
        {
            var time = (float)stopwatch.Elapsed.TotalSeconds;
            actionTiming = time;
            endTiming = time + 1.0f;
        }
    }
}
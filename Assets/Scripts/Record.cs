using System.Diagnostics;

namespace Shirahadori
{
    public class Record
    {
        public float actionTiming { get; set; }
        public float endTiming { get; set; }

        private Stopwatch stopwatch;        

        public Record(GameManager gameManager)
        {
            stopwatch = new Stopwatch();            
            gameManager.OnAction += OnAction;
            gameManager.OnStartAction += OnStartAction;
            gameManager.OnReset += OnReset;
        }

        private void OnStartAction()
        {
            stopwatch.Start();
        }

        private void OnAction()
        {
            var time = (float)stopwatch.Elapsed.TotalSeconds;
            actionTiming = time;
            endTiming = time + 1.0f;
        }

        private void OnReset()
        {
            stopwatch.Reset();
        }
    }
}
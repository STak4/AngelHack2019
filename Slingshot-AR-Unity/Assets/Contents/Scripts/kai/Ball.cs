using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace kai
{
    public class Ball : MonoBehaviour
    {
        public bool isLocal;
        // Start is called before the first frame update
        void Start()
        {
            Task.Delay(5000).ContinueWith(task => { Destroy(gameObject); }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

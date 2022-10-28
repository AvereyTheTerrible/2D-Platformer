using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.Feedback
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void CreateFeedback();

        public abstract void CompletePreviousFeedback();
    }
}
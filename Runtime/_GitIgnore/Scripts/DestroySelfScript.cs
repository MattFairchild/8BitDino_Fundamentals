using UnityEngine;

namespace EightBitDinosaur
{
    public class DestroySelfScript : MonoBehaviour
    {
        public void destroy()
        {
            Destroy(this.gameObject);
        }
    }
}


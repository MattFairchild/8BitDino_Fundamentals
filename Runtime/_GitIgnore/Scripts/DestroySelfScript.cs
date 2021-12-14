/*  (c) 2021 matthew fairchild
 *  
    simple destroy script that is mainly used to enable destruction of an object via UnityEvents in Inspector when needed
 */

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


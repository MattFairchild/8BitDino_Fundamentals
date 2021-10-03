/*  (c) 2021 matthew fairchild
 *  
 *  simple class as data holder for json serialized information for additional requirements made per project that are checked on open and save
 */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EightBitDinosaur
{
    [Serializable]
    public class Dino_EditorVerificationData
    {
        [SerializeField] public List<string> m_required_tags = new List<string>();
        [SerializeField] public List<string> m_required_layers = new List<string>();
        [SerializeField] public List<MonoScript> m_required_scripts = new List<MonoScript>();
    }
}


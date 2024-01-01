using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault.Interfaces
{
    public interface IWASDInput
    {
        public bool W_Key_Hold();
        public bool A_Key_Hold();
        public bool S_Key_Hold();
        public bool D_Key_Hold();
    }
}

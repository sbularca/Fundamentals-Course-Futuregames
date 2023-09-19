using System;
using UnityEngine;

namespace Examples {
    public class GameMenuController : MonoBehaviour {

        public ConstructorsExample constructorsExample;

        private void Start() {
            Menu.openMenu += OpenSubMenu;
        }
        private void OpenSubMenu() {
            // when openMenu is invoked from any other class (function/method), this will be invoked as well
        }
    }
}

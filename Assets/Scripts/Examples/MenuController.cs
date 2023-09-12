using System;
using UnityEngine;

namespace Examples {
    public class MenuController : MonoBehaviour {
        private Menu mainMenu = new Menu();
        private Menu inGameMenu = new Menu();

        // this is a shared/global variable
        // this is also called a class member variable
        private float timer;

        private void Start() {
            timer = 0f;
            mainMenu.numberOfButtons = 3;
            inGameMenu.numberOfButtons = 2;

            // this is a local variable
            Menu.Submenu subMenu = new Menu.Submenu();
            subMenu.subMenuPrefab = new GameObject();

            Menu.openMenu = Menu.openMenu + OpenMenu;
        }
        private void OpenMenu() {
            // add code to open the menu
        }

        private void Update() {
            if(timer >= 5f) {
                if (Menu.openMenu != null) {
                    Menu.openMenu.Invoke();
                }
            }
            timer += Time.deltaTime;
        }

        private void OnDisable() {
        }

        public void OnDestroy() {
            Menu.openMenu -= OpenMenu;
        }
    }
}

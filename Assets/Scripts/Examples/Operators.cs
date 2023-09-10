using UnityEngine;

public class Operators : MonoBehaviour {
    // private member
    private string playerName;

    public void Initialize() {
        NullChecks(new PlayerData("John Doe"), gameObject);
    }

    // private method
    private void NullChecks(PlayerData playerData, GameObject playerObject) {

        // Example 1
        if(playerData != null) {
            playerName = playerData.name;
        }
        playerName = playerData?.name;

        //Example 2
        if (playerData != null) {
            if (playerData.name != null) {
                playerName = playerData.name;
            }
        }
        playerName = playerData?.name ?? "Unknown"; // same as above using null conditional and null coalescence operators

        //Example 3
        if (playerName == null) {
            playerName = "Unknown";
        }
        playerName ??= "Unknown"; // this is the same as the line above

        //Example 4
        if(playerObject != null) {
            var playerTransform = playerObject.transform;
        }
        var playerTransformNc = playerObject?.transform;


        //Example 5 - null check alternative
        if(playerObject is not null) {
            //Do something;
        }

        //Example 6 - null check pattern
        if (playerData != null && playerData.name != null) {
            //Do something
        }

        if (playerData is { name: not null }) {
            //Do something
        }
    }
}

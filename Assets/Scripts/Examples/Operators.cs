using UnityEngine;

public class Operators : MonoBehaviour {
    // private member
    private string playerName;

    public void Initialize() {
        NullChecks(new CreatureData(), gameObject);
    }

    // private method
    private void NullChecks(CreatureData creatureData, GameObject playerObject) {

        // Example 1
        if(creatureData != null) {
            playerName = creatureData.name;
        }
        playerName = creatureData?.name;

        //Example 2
        if (creatureData != null) {
            if (creatureData.name != null) {
                playerName = creatureData.name;
                return;
            }
            playerName = "Unknown";
        }

        playerName = creatureData?.name ?? "Unknown"; // same as above using null conditional and null coalescence operators

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
        if (creatureData != null && creatureData.name != null) {
            //Do something
        }

        if (creatureData is { name: not null }) {
            //Do something
        }
    }
}

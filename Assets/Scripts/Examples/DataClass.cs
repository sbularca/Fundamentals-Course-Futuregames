using System.Collections.Generic;

public class DataClass {
    private readonly int nrOfLives;
    private readonly List<string> livesNames;
    private int currentNrOfLives;

    public bool resetLives;

    public int NumberOfLives {
        get {
            return currentNrOfLives;
        }
        set {
            currentNrOfLives = value;
            //call any function here
        }
    }

    // get property equivalent
    public int GetNrOfLives() {
        return nrOfLives;
    }

    // set property equivalent
    public void SetNumberOfLives(int nr) {
        currentNrOfLives = nr;
    }

    public int CurrentNrOfLives {
        get {
            return currentNrOfLives;
        }
    }

    public void ResetLives() {
    }

    public DataClass(int nrOfLives) {
        this.nrOfLives = nrOfLives;
    }

    public DataClass(int nrOfLives, List<string> livesNames) {
        this.nrOfLives = nrOfLives;
        this.livesNames = livesNames;
    }

    //use nr of lives to do something
    public void DecreaseLives() {
        currentNrOfLives = nrOfLives;
        currentNrOfLives--;
    }
}

using System;

[Serializable]
public class Record {
    public int record_id;
    public string player;
    public int highscore;

}

[Serializable]
public class Records {
    public Record[] records;
}
using ChartLoader.NET.Framework;
using ChartLoader.NET.Utils;
using System.IO;
using UnityEngine;

public class ChartLoaderLevel : MonoBehaviour
{
    public static ChartReader chartReader;
    public Transform[] notePrefabs;

    [SerializeField]
    private float _speed = 1f;
    /// <summary>
    /// The game speed.
    /// </summary>
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    [SerializeField]
    private NoteMovement _noteMovement;
    /// <summary>
    /// Camera movement aggregation.
    /// </summary>
    public NoteMovement NoteMovement
    {
        get
        {
            return _noteMovement;
        }
        set
        {
            _noteMovement = value;
        }
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        string CurrentDirectory = Directory.GetCurrentDirectory();
        AudioSource audio = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        transform.position = new Vector3(0, 1.5f, 0);
        chartReader = new ChartReader();
        Chart epicChart = chartReader.ReadChartFile(CurrentDirectory + "\\Assets\\ChartFiles\\Cartoon - On & On (feat. Daniel Levi)\\notes.chart");

        Note[] expertGuitarNotes = epicChart.GetNotes("ExpertSingle");
        SpawnNotes(expertGuitarNotes);
        //SpawnSynchTracks(epicChart.SynchTracks);

        transform.Rotate(0, 0, -90);

        StartSong(audio);
    }

    /// <summary>
    /// Spawn small notes
    /// </summary>
    public void SpawnNotes(Note[] notes)
    {
        foreach (Note note in notes)
        {
            SpawnNote(note);
        }
    }

    public void SpawnNote(Note note)
    {
        float hold = 0f;
        Vector3 point;
        Transform tmp;
        for (int i = 0; i < note.ButtonIndexes.Length; i++)
        {
            if (note.ButtonIndexes[i])
            {
                point = new Vector3(i - 2f, note.Seconds + hold, 0);
                tmp = SpawnPrefab(notePrefabs[i], point);
            }
        }
    }

    /// <summary>
    /// Spawns a prefab.
    /// </summary>
    public Transform SpawnPrefab(Transform prefab, Vector3 point)
    {
        Transform tmp = Instantiate(prefab);
        tmp.SetParent(transform);
        tmp.position = point;

        return tmp;
    }

    /// <summary>
    /// Starts playing the song.
    /// </summary>
    private void StartSong(AudioSource audio)
    {
        NoteMovement.Speed = Speed;
        NoteMovement.enabled = true;
        PlayMusic(audio);
    }

    /// <summary>
    /// Plays the current song.
    /// </summary>
    private void PlayMusic(AudioSource audio)
    {
        audio.Play();
    }
}

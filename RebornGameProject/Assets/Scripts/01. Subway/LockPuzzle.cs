using UnityEngine;

public class LockPuzzle : Puzzle
{
    [SerializeField, Tooltip("정답을 나타내는 4자리 비밀번호 (e.g. 1234,1122,3456)")]
    private string answer = "1423";

    private readonly int[] answerNumber = new int[4];
    private readonly LockNumber[] numbers = new LockNumber[4];
    private Canvas canvas;

    private void Start()
    {
        canvas = transform.RecursiveFind("Canvas").GetComponent<Canvas>();
        var numberObjects = transform.RecursiveFind("Numbers");
        var answerChar = answer.ToCharArray();
        for (int i = 0; i < 4; i++)
        {
            var numberObject = numberObjects.GetChild(i);
            numbers[i] = numberObject.gameObject.GetComponent<LockNumber>();
            numbers[i].Index = i;
            numbers[i].OnNumberUpdated = OnNumberUpdated;
            answerNumber[i] = answerChar[i] - '0';
        }
        canvas.enabled = false;
    }

    private void OnMouseDown()
    {
        if (IsEndPuzzle) return;
        canvas.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = false;
        }
    }

    public void OnNumberUpdated(LockNumber lockNumber)
    {
        for (int i = 0; i < 4; i++)
        {
            if (numbers[i].Number != answerNumber[i])
            {
                return;
            }
        }

        IsEndPuzzle = true;
        canvas.enabled = false;
    }
}
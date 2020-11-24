/// <summary>
/// 삭제 예정
/// </summary>

public class ItemFormat
{
    public string itemName;
    public int itemNumber;
    public string popUpImageName;
    public string invenImageName;
    public bool keepActive;
    public int[] combineItemNumbers;
    public int[] resultItemNumbers;

    public ItemFormat(string itemName, int itemNumber, string popUpImageName, string invenImageName, bool keepActive, int[] combineItemNumbers, int[] resultItemNumbers)
    {
        this.itemName = itemName;
        this.itemNumber = itemNumber;
        this.popUpImageName = popUpImageName;
        this.invenImageName = invenImageName;
        this.keepActive = keepActive;
        this.combineItemNumbers = combineItemNumbers;
        this.resultItemNumbers = resultItemNumbers;
    }
}

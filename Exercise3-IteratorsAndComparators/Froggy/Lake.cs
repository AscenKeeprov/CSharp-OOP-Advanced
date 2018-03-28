using System.Collections;
using System.Collections.Generic;

public class Lake : IEnumerable<int>
{
    private List<int> stones;

    public IEnumerator<int> GetEnumerator()
    {
	for (int s = 0; s < stones.Count; s += 2)
	    yield return stones[s];
	int lastPosition = stones.Count - 1;
	int lastOddPosition = lastPosition;
	if (lastPosition % 2 == 0) lastOddPosition--;
	for (int s = lastOddPosition; s >= 1; s -= 2)
	    if (s % 2 != 0) yield return stones[s];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
	return GetEnumerator();
    }

    public Lake(params int[] stones)
    {
	this.stones = new List<int>(stones);
    }
}

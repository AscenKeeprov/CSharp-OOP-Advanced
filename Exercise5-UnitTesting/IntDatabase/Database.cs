using System;
using System.Collections.Generic;
using System.Linq;

public class Database<T>
{
    private const int DATABASE_CAPACITY = 16;

    private T[] records;
    private int recordsCount;
    private int currentIndex;

    private Database()
    {
	records = new T[DATABASE_CAPACITY];
	currentIndex = -1;
    }

    public Database(IEnumerable<T> records) : this()
    {
	ValidateInput(records);
	recordsCount = records.Count();
	Array.Copy(records.ToArray(), this.records, recordsCount);
    }

    private void ValidateInput(IEnumerable<T> records)
    {
	if (typeof(T) != typeof(Int32))
	    throw new InvalidCastException("Database can only contain integer values!");
	if (records.Count() > DATABASE_CAPACITY)
	    throw new InvalidOperationException("Database capacity exceeded!");
    }

    public void Add(T record)
    {
	if (recordsCount == DATABASE_CAPACITY)
	    throw new InvalidOperationException("Database full!");
	currentIndex++;
	records[currentIndex] = record;
	recordsCount++;
    }

    public void Remove()
    {
	if (recordsCount == 0)
	    throw new InvalidOperationException("Database empty!");
	records[currentIndex] = default(T);
	currentIndex--;
	recordsCount--;
    }

    public T[] Fetch()
    {
	if (recordsCount == 0 || recordsCount == DATABASE_CAPACITY) return records;
	T[] recordsToFetch = new T[recordsCount];
	Array.Copy(records, recordsToFetch, recordsCount);
	return recordsToFetch;
    }
}

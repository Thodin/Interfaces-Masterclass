namespace HomeAutomation.CurrentTemp;

public interface IFilter
{
    void AddUnfilteredValue(double value);
    double GetFilteredValue();
}

public class MovingAverageFilter(int filterSize) : IFilter
{
    private readonly int _filterSize = filterSize;
    // Array to hold all the values in the filter. Default initialization to zeros.
    // We only need this for knowing which old value needs to be removed when a 
    // new one comes in.
    private readonly double[] values = new double[filterSize];
    // The sum of all values.
    private double sum = 0.0;
    // The index that will be written to next in the values array.
    private int indexToWriteNext = 0;

    public void AddUnfilteredValue(double value)
    {
        // Update the sum.
        sum = sum - values[indexToWriteNext] + value;
        // Add to the values array.
        values[indexToWriteNext] = value;
        // Update the next to write index, wrap around.
        indexToWriteNext = (indexToWriteNext + 1) % _filterSize;
    }

    public double GetFilteredValue()
    {
        // There is a tradeoff to be made here:
        // Either hold the sum as member and divide by filter size when the average is queried,
        // or hold the average as member, which requires the division when a value is added.
        // It is preferred to do the division in the action that is expected to occurr less often.
        // In our example, #reads = #writes, thus it doesn't matter.
        return sum / _filterSize;
    }
}

public class DeadbandFilter(double bandwidth) : IFilter
{
    private double _filteredValue = double.PositiveInfinity;
    private readonly double _bandwidth = bandwidth;

    public void AddUnfilteredValue(double value)
    {
        if (Math.Abs(value - _filteredValue) > _bandwidth)
        {
            _filteredValue = value;
        }
    }

    public double GetFilteredValue()
    {
        return _filteredValue;
    }
}
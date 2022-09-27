var perfectNumbers = Enumerable.Range(2, 25)
    .Where(x => Enumerable.Range(2, (int)Math.Sqrt(Math.Pow(2, x)) + 1)
        .Where(divisor => ((int)Math.Pow(2, x) - 1) % divisor == 0)
        .Sum() == 0)
    .AsParallel()
    .Select(x => Math.Pow(2, x - 1) * (Math.Pow(2, x) - 1))
    .ToArray();


foreach (var perfectNumber in perfectNumbers)
{
    Console.WriteLine(perfectNumber);
}
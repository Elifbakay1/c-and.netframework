using System;

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create shipping service and start the quote process
        var shippingService = new ShippingService();
        shippingService.ProcessShippingQuote();
    }
}

// Class representing a package with its dimensions and weight
class Package
{
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }

    // Calculate total dimensions of the package
    public double TotalDimensions => Width + Height + Length;

    // Calculate shipping cost based on package properties
    public double CalculateShippingCost()
    {
        return (Width * Height * Length * Weight) / 100;
    }
}

// Class handling package validation rules
class PackageValidator
{
    private const int MaxWeight = 50;
    private const int MaxTotalDimensions = 50;

    // Validate package weight
    public bool IsWeightValid(double weight)
    {
        return weight <= MaxWeight;
    }

    // Validate package dimensions
    public bool AreDimensionsValid(double totalDimensions)
    {
        return totalDimensions <= MaxTotalDimensions;
    }
}

// Class handling the shipping service operations
class ShippingService
{
    private readonly PackageValidator _validator;
    private readonly Package _package;

    public ShippingService()
    {
        _validator = new PackageValidator();
        _package = new Package();
    }

    // Process the shipping quote request
    public void ProcessShippingQuote()
    {
        // Display welcome message
        Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

        // Get and validate weight
        if (!GetAndValidateWeight())
            return;

        // Get and validate dimensions
        if (!GetAndValidateDimensions())
            return;

        // Calculate and display quote
        DisplayShippingQuote();
    }

    // Get and validate package weight
    private bool GetAndValidateWeight()
    {
        Console.WriteLine("Please enter the package weight:");
        _package.Weight = Convert.ToDouble(Console.ReadLine());

        if (!_validator.IsWeightValid(_package.Weight))
        {
            Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
            return false;
        }
        return true;
    }

    // Get and validate package dimensions
    private bool GetAndValidateDimensions()
    {
        Console.WriteLine("Please enter the package width:");
        _package.Width = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Please enter the package height:");
        _package.Height = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Please enter the package length:");
        _package.Length = Convert.ToDouble(Console.ReadLine());

        if (!_validator.AreDimensionsValid(_package.TotalDimensions))
        {
            Console.WriteLine("Package too big to be shipped via Package Express.");
            return false;
        }
        return true;
    }

    // Display the calculated shipping quote
    private void DisplayShippingQuote()
    {
        double quote = _package.CalculateShippingCost();
        Console.WriteLine($"Your estimated total for shipping this package is: ${quote:F2}");
        Console.WriteLine("Thank you!");
    }
}
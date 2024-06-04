using System;

public class Superposition
{
    public static void Main(string[] args)
    {
        int R1, R2, R3;
        double E1, E2, difference;
        R1 = 110;
        R2 = 200;
        R3 = 430;
        Dictionary<string, double[]> calculates;
       
        for(int i = 1; i <= 10; i ++)
        {
            Console.WriteLine($"\nIteration number: {i}\n\nEnter E1: ");
            E1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter E2: ");
            E2 = double.Parse(Console.ReadLine());
            calculates = CalculateCircuit(E1, E2, R1, R2, R3);

            
            foreach(KeyValuePair<string, double[]> kvp in calculates)
            {
                for(int j = 0; j <= 2; j++)
                {
                    Console.WriteLine($"\n\nEnter Experymental {kvp.Key} | {j} value: ");
                    difference = GetRelativeProcentError(kvp.Value[j], double.Parse(Console.ReadLine()));
                    Console.WriteLine($"Difference in {kvp.Key} | {j}: {difference}%");
                }
            }


        }
    }

    static Dictionary<string, double[]> CalculateCircuit(double E1, double E2, int R1, int R2, int R3)
    {
        double R_Modified_1, R_Modified_2, I1_a, I2_a, I3_a, I1_b, I2_b, I3_b, I1, I2, I3;
        R_Modified_1 = (R2 * R3) / (R2 + R3);
        R_Modified_2 = R1 + R_Modified_1;
        I1_a = Math.Round(1000 * E1 / R_Modified_2, 2);
        I2_a = Math.Round(I1_a * R_Modified_1 / R2, 2);
        I3_a = Math.Round(I1_a * R_Modified_1 / R3, 2);

        // First Result and Calculations
        Console.WriteLine($"First Circuit Result:\nI1 = {I1_a}\nI2 = {I2_a}\nI3 = {I3_a}\n");

        R_Modified_1 = (R1 * R3) / (R1 + R3);
        R_Modified_2 = R2 + R_Modified_1;

        I2_b = Math.Round(-1000 * E2 / R_Modified_2, 2);
        I1_b = Math.Round(I2_b * R_Modified_1 / R1, 2);
        I3_b = Math.Round(-I2_b * R_Modified_1 / R3, 2);
        Console.WriteLine($"Second Circuit Result:\nI1 = {I1_b}\nI2 = {I2_b}\nI3 = {I3_b}\n");


        I1 = I1_a + I1_b;
        I2 = I2_a + I2_b;
        I3 = I3_a + I3_b;

        Console.WriteLine($"Third Circuit Result:\nI1 = {I1}\nI2 = {I2}\nI3 = {I3}\n");

        return new Dictionary<string, double[]>()
        {
            { "E1", new double[] {I1_a, I2_a, I3_a} },
            { "E2", new double[] {I1_b, I2_b, I3_b} },
            { "E23", new double[] {I1,I2,I3} },
        };
    }

    static double GetRelativeProcentError(double I_teory, double I_real)
    {
        return Math.Round((Math.Abs(I_teory - I_real) / Math.Abs(I_teory)) * 100, 2);
    }
}
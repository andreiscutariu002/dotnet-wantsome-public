namespace Code.Formatting.BreakingLongLines
{
    public class MatricesHelper
    {
        public void MatrixCheck(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            for (var k = 0; k < matrix.GetLength(1); k++)
                if (matrix[i, k] == 0 ||
                    matrix[i + 1, k] == 0 ||
                    matrix[i, k + 1] == 0 ||
                    matrix[i + 1, k + 1] == 0)
                {
                    // .....
                }
        }
    }
}

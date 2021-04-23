namespace desafio_rdi.domain.Utils
{
    public class ArrayRotation
    {
        public static string RightRotate(int[] a, int times)
        {
            for (int i = 0; i < times; i++)
            {
                RightRotateByOne(a);
            }

            return string.Join("", a);
        }

        private static void RightRotateByOne(int[] a)
        {
            int last = a[a.Length - 1];

            for (int i = a.Length - 2; i >= 0; i--)
            {
                a[i + 1] = a[i];
            }
            a[0] = last;
        }
    }
}

using desafio_rdi.domain.Utils;
using FluentAssertions;
using Xunit;

namespace desafio_rdi_tests.Scenarios.Unit
{
    public  class CircularRotationTest
    {
        [Fact]
        public  void Should_Rotate_Twice()
        {
            int timesToRotate = 2;
            int[] firtSituation = {3,4,5 };

            var expected =  "453";
            var rotation = ArrayRotation.RightRotate(firtSituation, timesToRotate);
            rotation.Should().Be(expected);
            
            int[] secondSituation = { 1, 2, 3 };
            
            expected = "231";
            rotation = ArrayRotation.RightRotate(secondSituation, timesToRotate);
            rotation.Should().Be(expected);

            int[] thirdSituation = { 8,2,3 };
                                   
            expected = "238";
            rotation = ArrayRotation.RightRotate(thirdSituation, timesToRotate);
            rotation.Should().Be(expected);
        }
    }
}

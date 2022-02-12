using Xunit;

namespace Nucleo.Tests
{
    public class Chunk2Tests
    {
        [Fact]
        public void Test1()
        {
            var x = "abcdef"
                        .Chunk2(size: 1, step: 1)
                        .ToList();

            Assert.Equal(@"[[""a""],[""b""],[""c""],[""d""],[""e""],[""f""]]", JsonSerializer.Serialize(x));
        }

        [Fact]
        public void Test2()
        {
            var x = "abcdef"
                        .Chunk2(size: 2, step: 1)
                        .ToList();

            Assert.Equal(@"[[""a"",""b""],[""b"",""c""],[""c"",""d""],[""d"",""e""],[""e"",""f""]]", JsonSerializer.Serialize(x));
        }

        [Fact]
        public void Test3()
        {
            var x = "abcdef"
                        .Chunk2(size: 2, step: 2)
                        .ToList();

            Assert.Equal(@"[[""a"",""b""],[""c"",""d""],[""e"",""f""]]", JsonSerializer.Serialize(x));
        }


        [Fact]
        public void Test4()
        {
            var x = "abcdef"
                        .Chunk2(size: 4, step: 1)
                        .ToList();

            Assert.Equal(@"[[""a"",""b"",""c"",""d""],[""b"",""c"",""d"",""e""],[""c"",""d"",""e"",""f""]]", JsonSerializer.Serialize(x));
        }

        [Fact]
        public void Test5()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var _ = "abcdef"
                    .Chunk2(size: 2, step: 10)
                    .ToList();

            });
        }
    }
}

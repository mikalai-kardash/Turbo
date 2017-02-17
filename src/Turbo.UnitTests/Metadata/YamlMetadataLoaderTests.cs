using Microsoft.VisualStudio.TestTools.UnitTesting;
using Turbo.UnitTests.PageObjects.Google;
using Turbo.UnitTests.PageObjects.Google.Pages.Search;
using Turbo.UnitTests.PageObjects.Google.Pages.Search.Parts.Header;
using Turbo.UnitTests.PageObjects.Google.Pages.Search.Parts.MetaNotEmbedded;
using Turbo.UnitTests.PageObjects.Google.Pages.Search.Parts.NoMeta;

namespace Turbo.UnitTests.Metadata
{
    [TestClass]
    public class YamlMetadataLoaderTests
    {
        private YamlMetadataLoader _loader;

        [TestInitialize]
        public void SetUp()
        {
            _loader = new YamlMetadataLoader();
        }

        [TestMethod]
        public void Loads_App_meta()
        {
            var meta = _loader.GetAppMeta<GoogleSearchApp>();

            Assert.IsNotNull(meta);
            Assert.AreEqual(typeof(GoogleSearchApp), meta.Type);
            Assert.IsNotNull(meta.Meta.Url);
        }

        [TestMethod]
        public void Loads_Page_meta()
        {
            var meta = _loader.GetPageMeta<SearchPage>();

            Assert.IsNotNull(meta);
            Assert.AreEqual(typeof(SearchPage), meta.Type);
            Assert.AreEqual("/", meta.Meta.Url);
        }

        [TestMethod]
        public void Loads_Part_meta()
        {
            var meta = _loader.GetPartMeta<Header>();

            Assert.IsNotNull(meta);
            Assert.AreEqual(typeof(Header), meta.Type);
            Assert.IsNotNull(meta.Meta.Selector);
        }

        [TestMethod]
        public void DoesNotThrow_When_meta_file_is_missing()
        {
            var meta = _loader.GetPartMeta<NoMeta>();

            Assert.IsNotNull(meta);
            Assert.IsNull(meta.Meta);
        }

        [TestMethod]
        public void DoesNotThrow_When_meta_file_is_not_embedded()
        {
            var meta = _loader.GetPartMeta<MetaNotEmbedded>();

            Assert.IsNotNull(meta);
            Assert.IsNull(meta.Meta);
        }
    }
}
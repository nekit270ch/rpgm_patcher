using System;
using System.IO;
using System.Text;

namespace RPGMPatcher{
    class Patcher{
        static string patcherUtilsCode = "Y29uc3QgZnMgPSByZXF1aXJlKCdmcycpOw0KY29uc3QgcCA9IHJlcXVpcmUoJ3BhdGgnKTsNCg0KaWYoIWZzLmV4aXN0c1N5bmMoJ3d3dy90ZXh0dXJlcGFja3MnKSl7DQoJZnMubWtkaXJTeW5jKCd3d3cvdGV4dHVyZXBhY2tzJyk7DQp9DQoNCmlmKCFmcy5leGlzdHNTeW5jKCd3d3cvanMvY3VzdG9tU2NyaXB0cycpKXsNCglmcy5ta2RpclN5bmMoJ3d3dy9qcy9jdXN0b21TY3JpcHRzJyk7DQp9DQoNCnRyeXsNCgl3aW5kb3cucGNvbmYgPSBKU09OLnBhcnNlKGZzLnJlYWRGaWxlU3luYygnd3d3L3BhdGNoZXIuanNvbicpKTsNCn1jYXRjaChfKXsNCgl3aW5kb3cucGNvbmYgPSB7DQoJCXRleHR1cmVfcGFjazogJ25vbmUnLA0KCQlrZXlzOiB7DQoJCQlQQVVTRTogJ3AnLA0KCQkJU0FWRTogJ3MnLA0KCQkJUkVTVEFSVDogJ3InLA0KCQkJSlNfQ09OU09MRTogJ2onLA0KCQkJR09EX01PREU6ICdnJywNCgkJCUFERF9NT05FWTogJ20nLA0KCQkJTk9fQ0xJUDogJ24nLA0KCQkJU0VUX1NQRUVEOiAnZScsDQoJCQlTRVRfVEVYVFVSRV9QQUNLOiAndCcsDQoJCQlIRUxQOiAnaCcNCgkJfQ0KCX0NCglmcy53cml0ZUZpbGVTeW5jKCd3d3cvcGF0Y2hlci5qc29uJywgSlNPTi5zdHJpbmdpZnkocGNvbmYsIG51bGwsIDQpKTsNCn0NCg0Kd2luZG93LmxvYWRlZFRleHR1cmVzID0gW107DQp3aW5kb3cuZ29kTW9kZSA9IGZhbHNlOw0KbGV0IG5vY2xpcCA9IGZhbHNlOw0KDQp3aW5kb3cuYWRkRXZlbnRMaXN0ZW5lcignbG9hZCcsICgpPT57DQoJZnMucmVhZGRpclN5bmMoJ3d3dy9qcy9jdXN0b21TY3JpcHRzJykuZm9yRWFjaChmPT57DQoJCWlmKCFmLmVuZHNXaXRoKCcuanMnKSkgcmV0dXJuOw0KCQlsZXQgcyA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ3NjcmlwdCcpOw0KCQlzLnNyYyA9ICdqcy9jdXN0b21TY3JpcHRzLycgKyBmOw0KCQlkb2N1bWVudC5ib2R5LmFwcGVuZENoaWxkKHMpOw0KCX0pOw0KfSk7DQoNCndpbmRvdy5hZGRFdmVudExpc3RlbmVyKCdrZXlwcmVzcycsIGU9PnsNCglzd2l0Y2goZS5rZXkpew0KCQljYXNlIHBjb25mLmtleXMuSlNfQ09OU09MRTogew0KCQkJbGV0IGlucCA9IHByb21wdCgn0JLQstC10LTQuNGC0LUg0LrQvtC80LDQvdC00YMnKTsNCgkJCWlmKCFpbnAgfHwgaW5wLmxlbmd0aCA9PSAwKSByZXR1cm47DQoJCQl0cnl7DQoJCQkJYWxlcnQoZXZhbChpbnApKTsNCgkJCX1jYXRjaChlKXsNCgkJCQlhbGVydChlKTsNCgkJCX0NCgkJCWJyZWFrOw0KCQl9DQoNCgkJY2FzZSBwY29uZi5rZXlzLlNBVkU6IHsNCgkJCVNjZW5lTWFuYWdlci5wdXNoKFNjZW5lX1NhdmUpOw0KCQkJYnJlYWs7DQoJCX0NCg0KCQljYXNlIHBjb25mLmtleXMuR09EX01PREU6IHsNCgkJCXdpbmRvdy5nb2RNb2RlID0gdHJ1ZTsNCgkJCWFsZXJ0KCfQoNC10LbQuNC8INCx0L7Qs9CwINCy0LrQu9GO0YfQtdC9Jyk7DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5BRERfTU9ORVk6IHsNCgkJCSRnYW1lUGFydHkuX2dvbGQgPSBOdW1iZXIuTUFYX1NBRkVfSU5URUdFUjsNCgkJCWFsZXJ0KCfQlNC10L3RjNCz0Lgg0L3QsNC60YDRg9GH0LXQvdGLJyk7DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5QQVVTRTogew0KCQkJYWxlcnQoJ9Cf0JDQo9CX0JBcblxu0J3QsNC20LzQuNGC0LUg0J7Qmiwg0YfRgtC+0LHRiyDQstC+0LfQvtCx0L3QvtCy0LjRgtGMJyk7DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5SRVNUQVJUOiB7DQoJCQlsb2NhdGlvbi5yZWxvYWQoKTsNCgkJCWJyZWFrOw0KCQl9DQoNCgkJY2FzZSBwY29uZi5rZXlzLk5PX0NMSVA6IHsNCgkJCW5vY2xpcCA9ICFub2NsaXA7DQoJCQlpZihub2NsaXApew0KCQkJCSRnYW1lUGxheWVyLnJDYW5QYXNzID0gJGdhbWVQbGF5ZXIuY2FuUGFzczsNCgkJCQkkZ2FtZVBsYXllci5jYW5QYXNzID0gKCk9PnRydWU7DQoJCQl9ZWxzZXsNCgkJCQkkZ2FtZVBsYXllci5jYW5QYXNzID0gJGdhbWVQbGF5ZXIuckNhblBhc3M7DQoJCQl9DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5TRVRfU1BFRUQ6IHsNCgkJCWxldCBpbnAgPSBwcm9tcHQoJ9CS0LLQtdC00LjRgtC1INC90L7QstGD0Y4g0YHQutC+0YDQvtGB0YLRjCcsICRnYW1lUGxheWVyLl9tb3ZlU3BlZWQpOw0KCQkJaWYoaW5wICYmICFpc05hTigraW5wKSl7DQoJCQkJJGdhbWVQbGF5ZXIuX21vdmVTcGVlZCA9ICtpbnA7DQoJCQl9DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5TRVRfVEVYVFVSRV9QQUNLOiB7DQoJCQl0ZXh0dXJlUGFja3NVSSgpOw0KCQkJYnJlYWs7DQoJCX0NCg0KCQljYXNlIHBjb25mLmtleXMuSEVMUDogew0KCQkJbGV0IHN0ciA9ICfQmtC70LDQstC40YjQuCBSUEdNIFBhdGNoZXJcblxuJzsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5QQVVTRS50b1VwcGVyQ2FzZSgpfV0g0J/QvtGB0YLQsNCy0LjRgtGMINC40LPRgNGDINC90LAg0L/QsNGD0LfRg1xuYDsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5SRVNUQVJULnRvVXBwZXJDYXNlKCl9XSDQn9C10YDQtdC30LDQv9GD0YHRgtC40YLRjCDQuNCz0YDRg1xuYDsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5KU19DT05TT0xFLnRvVXBwZXJDYXNlKCl9XSDQntGC0LrRgNGL0YLRjCDQutC+0L3RgdC+0LvRjCBKYXZhU2NyaXB0XG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLlNBVkUudG9VcHBlckNhc2UoKX1dINCe0YLQutGA0YvRgtGMINC+0LrQvdC+INGB0L7RhdGA0LDQvdC10L3QuNGPXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLlNFVF9URVhUVVJFX1BBQ0sudG9VcHBlckNhc2UoKX1dINCe0YLQutGA0YvRgtGMINC+0LrQvdC+INCy0YvQsdC+0YDQsCDRgtC10LrRgdGC0YPRgNC/0LDQutCwXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLkdPRF9NT0RFLnRvVXBwZXJDYXNlKCl9XSDQoNC10LbQuNC8INCx0L7Qs9CwICjQvtGH0LXQvdGMINCy0YvRgdC+0LrQuNC1IEhQLCDQsNGC0LDQutCwLCDQt9Cw0YnQuNGC0LAg0Lgg0YIu0LQuKVxuYDsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5BRERfTU9ORVkudG9VcHBlckNhc2UoKX1dINCd0LDQutGA0YPRgtC60LAg0LTQtdC90LXQs1xuYDsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5OT19DTElQLnRvVXBwZXJDYXNlKCl9XSBOb0NsaXAgKNCy0L7Qt9C80L7QttC90L7RgdGC0Ywg0L/RgNC+0YXQvtC00LjRgtGMINGB0LrQstC+0LfRjCDQvtCx0YrQtdC60YLRiylcbmA7DQoJCQlzdHIgKz0gYFske3Bjb25mLmtleXMuU0VUX1NQRUVELnRvVXBwZXJDYXNlKCl9XSDQmNC30LzQtdC90LjRgtGMINGB0LrQvtGA0L7RgdGC0Ywg0LjQs9GA0L7QutCwXG5gOw0KCQkJYWxlcnQoc3RyKTsNCgkJfQ0KCX0NCn0pOw0KDQpDU1NTdHlsZURlY2xhcmF0aW9uLnByb3RvdHlwZS5zZXQgPSBmdW5jdGlvbihvYmopew0KCWZvcihsZXQgaSBpbiBvYmopew0KCQl0aGlzW2ldID0gb2JqW2ldOw0KCX0NCn0NCg0KU3RyaW5nLnByb3RvdHlwZS5yZXBsYWNlQWxsID0gZnVuY3Rpb24obywgbil7DQoJcmV0dXJuIHRoaXMuc3BsaXQobykuam9pbihuKTsNCn0NCg0KZnVuY3Rpb24gcG9wdXAob3B0cyl7DQoJaWYoIW9wdHMpIG9wdHMgPSB7fTsNCg0KCWxldCBoID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnZGl2Jyk7DQoJaC5zdHlsZS5zZXQoew0KCQlwb3NpdGlvbjogJ2Fic29sdXRlJywNCgkJbGVmdDogJzAnLA0KCQl0b3A6ICcwJywNCgkJd2lkdGg6ICcxMDB2dycsDQoJCWhlaWdodDogJzEwMHZoJywNCgkJekluZGV4OiAnOTk5OTk5JywNCgkJZGlzcGxheTogJ2ZsZXgnLA0KCQlqdXN0aWZ5Q29udGVudDogJ2NlbnRlcicsDQoJCWFsaWduSXRlbXM6ICdjZW50ZXInDQoJfSk7DQoJaC5hZGRFdmVudExpc3RlbmVyKCdjbGljaycsIGU9PnsNCgkJaWYoZS50YXJnZXQgPT0gaCl7DQoJCQlkb2N1bWVudC5ib2R5LnJlbW92ZUNoaWxkKGgpOw0KCQkJaWYob3B0cy5vbmNsb3NlKSBvcHRzLm9uY2xvc2UoKTsNCgkJfQ0KCX0pOw0KDQoJbGV0IGJveCA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ2RpdicpOw0KCWJveC5zdHlsZS5zZXQoew0KCQlib3JkZXI6ICdzb2xpZCAxcHggYmxhY2snLA0KCQlwYWRkaW5nOiAnMWVtJywNCgkJYmFja2dyb3VuZDogJ3doaXRlJywNCgkJY3Vyc29yOiAnZGVmYXVsdCcsDQoJCWZvbnRGYW1pbHk6ICdDb25zb2xhcywgbW9ub3NwYWNlJw0KCX0pOw0KDQoJaC5hcHBlbmRDaGlsZChib3gpOw0KDQoJcmV0dXJuIHsgaDogaCwgYm94OiBib3gsIHNob3c6ICgpPT57IGRvY3VtZW50LmJvZHkuYXBwZW5kQ2hpbGQoaCkgfSwgY2xvc2U6ICgpPT57IGRvY3VtZW50LmJvZHkucmVtb3ZlQ2hpbGQoaCkgfSB9Ow0KfQ0KDQpmdW5jdGlvbiByZWFkRGlyKGRpcil7DQoJbGV0IGFyciA9IFtdOw0KDQoJZnMucmVhZGRpclN5bmMoZGlyKS5mb3JFYWNoKGY9PnsNCgkJbGV0IG4gPSBwLmpvaW4oZGlyLCBmKTsNCgkJaWYoZnMuc3RhdFN5bmMobikuaXNEaXJlY3RvcnkoKSl7DQoJCQlhcnIucHVzaCguLi5yZWFkRGlyKG4pKTsNCgkJfWVsc2V7DQoJCQlhcnIucHVzaChuKTsNCgkJfQ0KCX0pOw0KDQoJcmV0dXJuIGFycjsNCn0NCg0KZnVuY3Rpb24gZ2V0U3ViRGlycyhkaXIpew0KCWxldCBhcnIgPSBbXTsNCg0KCWZzLnJlYWRkaXJTeW5jKGRpcikuZm9yRWFjaChmPT57DQoJCWxldCBuID0gcC5qb2luKGRpciwgZik7DQoJCWlmKGZzLnN0YXRTeW5jKG4pLmlzRGlyZWN0b3J5KCkpew0KCQkJYXJyLnB1c2gobik7DQoJCQlhcnIucHVzaCguLi5nZXRTdWJEaXJzKG4pKTsNCgkJfQ0KCX0pOw0KDQoJcmV0dXJuIGFycjsNCn0NCg0KZnVuY3Rpb24gdGV4dHVyZVBhY2tzVUkoKXsNCglsZXQgeyBib3gsIHNob3csIGNsb3NlIH0gPSBwb3B1cCgpOw0KCQ0KCWxldCBoZWFkZXIgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdoMicpOw0KCWhlYWRlci5zdHlsZS5tYXJnaW4gPSAnMCAwIDAuNWVtIDAuNWVtJzsNCgloZWFkZXIuaW5uZXJUZXh0ID0gJ9Ci0LXQutGB0YLRg9GA0L/QsNC60LgnOw0KCQ0KCWxldCBzZWwgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdzZWxlY3QnKTsNCglzZWwuc3R5bGUuc2V0KHsgd2lkdGg6ICcyMGVtJywgbWFyZ2luQm90dG9tOiAnMWVtJyB9KTsNCglzZWwuc2l6ZSA9IDc7DQoJc2VsLm9wdGlvbnNbMF0gPSBuZXcgT3B0aW9uKCc80L3QtdGCPicsICdub25lJyk7DQoJc2VsLmZvY3VzKCk7DQoJDQoJZnMucmVhZGRpclN5bmMoJ3d3dy90ZXh0dXJlcGFja3MnKS5mb3JFYWNoKChkLGkpPT57DQoJCXNlbC5vcHRpb25zW2krMV0gPSBuZXcgT3B0aW9uKGQsIGQpOw0KCX0pOw0KCXNlbC52YWx1ZSA9IHBjb25mLnRleHR1cmVfcGFjazsNCg0KCWxldCBidG5TZWwgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdidXR0b24nKTsNCglidG5TZWwuc3R5bGUuc2V0KHsgcGFkZGluZzogJzAuMmVtIDAuNWVtJywgbWFyZ2luUmlnaHQ6ICcwLjVlbScgfSk7DQoJYnRuU2VsLmlubmVyVGV4dCA9ICfQn9GA0LjQvNC10L3QuNGC0YwnOw0KDQoJYnRuU2VsLmFkZEV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgKCk9PnsNCgkJcGNvbmYudGV4dHVyZV9wYWNrID0gc2VsLnZhbHVlOw0KCQlmcy53cml0ZUZpbGVTeW5jKCd3d3cvcGF0Y2hlci5qc29uJywgSlNPTi5zdHJpbmdpZnkocGNvbmYsIG51bGwsIDQpKTsNCgkJbG9jYXRpb24ucmVsb2FkKCk7DQoJfSk7DQoNCglsZXQgYnRuQ3JlYXRlID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnYnV0dG9uJyk7DQoJYnRuQ3JlYXRlLnN0eWxlLnBhZGRpbmcgPSAnMC4yZW0gMC41ZW0nOw0KCWJ0bkNyZWF0ZS5pbm5lclRleHQgPSAn0KHQvtC30LTQsNGC0Ywg0L3QvtCy0YvQuSc7DQoNCglidG5DcmVhdGUuYWRkRXZlbnRMaXN0ZW5lcignY2xpY2snLCAoKT0+ew0KCQlsZXQgbmFtZSA9IHByb21wdCgn0JLQstC10LTQuNGC0LUg0L3QsNC30LLQsNC90LjQtSDQvdC+0LLQvtCz0L4g0YLQtdC60YHRgtGD0YDQv9Cw0LrQsCcsIGRvY3VtZW50LnRpdGxlKydfTmV3MScpOw0KCQlpZihuYW1lICYmIG5hbWUubGVuZ3RoID4gMCl7DQoJCQlmcy5ta2RpclN5bmMoJ3d3dy90ZXh0dXJlcGFja3MvJyArIG5hbWUpOw0KCQkJZGVjcnlwdEdhbWVGaWxlcygndGV4dHVyZXBhY2tzLycgKyBuYW1lLCAoKT0+ew0KCQkJCWNsb3NlKCk7DQoJCQkJdGV4dHVyZVBhY2tzVUkoKTsNCgkJCX0pOw0KCQl9DQoJfSk7DQoNCglib3guYXBwZW5kQ2hpbGQoaGVhZGVyKTsNCglib3guYXBwZW5kQ2hpbGQoc2VsKTsNCglib3guYXBwZW5kQ2hpbGQoZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnYnInKSk7DQoJYm94LmFwcGVuZENoaWxkKGJ0blNlbCk7DQoJYm94LmFwcGVuZENoaWxkKGJ0bkNyZWF0ZSk7DQoJc2hvdygpOw0KfQ0KDQpmdW5jdGlvbiBkZWNyeXB0R2FtZUZpbGVzKGRlc3REaXIsIG9uZW5kKXsNCglsZXQgeyBib3gsIHNob3cgfSA9IHBvcHVwKHsgb25jbG9zZTogb25lbmQgfSk7DQoNCglsZXQgaGVhZGVyID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnaDInKTsNCgloZWFkZXIuc3R5bGUubWFyZ2luID0gJzAgMCAwLjVlbSAwLjVlbSc7DQoJaGVhZGVyLmlubmVyVGV4dCA9ICfQodC+0LfQtNCw0L3QuNC1INGC0LXQutGB0YLRg9GA0L/QsNC60LAnOw0KDQoJbGV0IGxvZ3QgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCd0ZXh0YXJlYScpOw0KCWxvZ3QucmVhZE9ubHkgPSB0cnVlOw0KCWxvZ3Quc3R5bGUuc2V0KHsgd2lkdGg6ICczMGVtJywgaGVpZ2h0OiAnMTBlbScgfSk7DQoNCglib3guYXBwZW5kQ2hpbGQoaGVhZGVyKTsNCglib3guYXBwZW5kQ2hpbGQobG9ndCk7DQoJc2hvdygpOw0KDQoJZnVuY3Rpb24gbG9nKHRleHQpew0KCQlsb2d0LnZhbHVlICs9IHRleHQgKyAnXG4nOw0KCQlsb2d0LnNjcm9sbFRvKDAsIGxvZ3Quc2Nyb2xsSGVpZ2h0KTsNCgl9DQoNCglsb2coJ9Ch0L7Qt9C00LDQvdC40LUg0L/QsNC/0L7Qui4uLicpOw0KCWdldFN1YkRpcnMoJ3d3dy9pbWcnKS5mb3JFYWNoKGQ9PnsNCgkJbGV0IGRyID0gZC5yZXBsYWNlQWxsKCdcXCcsICcvJykucmVwbGFjZSgnaW1nJywgZGVzdERpcik7DQoJCWlmKCFmcy5leGlzdHNTeW5jKGRyKSl7DQoJCQlmcy5ta2RpclN5bmMoZHIpOw0KCQkJbG9nKCfQodC+0LfQtNCw0L3QsCDQv9Cw0L/QutCwOiAnICsgZHIpOw0KCQl9DQoJfSk7DQoNCglsZXQgZmlsZXMgPSByZWFkRGlyKCd3d3cvaW1nJyk7DQoJaWYoZmlsZXMuZmluZChuPT5uLmVuZHNXaXRoKCcucnBnbXZwJykpKXsNCgkJbG9nKCfQotC10LrRgdGC0YPRgNGLINC30LDRiNC40YTRgNC+0LLQsNC90YsuINCg0LDRgdGI0LjRhNGA0L7QstC60LAg0Lgg0LrQvtC/0LjRgNC+0LLQsNC90LjQtS4uLicpOw0KDQoJCWxldCBwdHAgPSBwY29uZi50ZXh0dXJlX3BhY2s7DQoJCXBjb25mLnRleHR1cmVfcGFjayA9ICdub25lJzsNCg0KCQlmdW5jdGlvbiBkZihpKXsNCgkJCWxldCBuID0gZmlsZXNbaV07DQoNCgkJCWlmKCFuLmVuZHNXaXRoKCcucnBnbXZwJykpew0KCQkJCWxldCBkc3QgPSBuLnJlcGxhY2UoJ2ltZycsIGRlc3REaXIpOw0KCQkJCWZzLmNvcHlGaWxlU3luYyhuLCBkc3QpOw0KCQkJCWxvZyhg0KTQsNC50LsgJHtufSDRgdC60L7Qv9C40YDQvtCy0LDQvSDQsiAke2RzdH1gKTsNCgkJCQlpZihpKzEgPCBmaWxlcy5sZW5ndGgpew0KCQkJCQlkZihpKzEpOw0KCQkJCX1lbHNlew0KCQkJCQlsb2coJ9Ci0LXQutGB0YLRg9GA0L/QsNC6INGB0L7Qt9C00LDQvScpOw0KCQkJCX0NCgkJCQlyZXR1cm47DQoJCQl9DQoNCgkJCWxvZygn0KDQsNGB0YjQuNGE0YDQvtCy0LrQsCDRhNCw0LnQu9CwOiAnICsgbik7DQoNCgkJCWxldCBubSA9IG4ucmVwbGFjZUFsbCgnXFwnLCAnLycpLnJlcGxhY2UoJ3d3dy8nLCAnJykucmVwbGFjZSgnLnJwZ212cCcsICcucG5nJyk7DQoNCgkJCWxldCBpbWcgPSBuZXcgSW1hZ2UoKTsNCgkJCWxldCBiaXRtYXAgPSB7IF9pbWFnZTogaW1nLCBfb25Mb2FkOiAoKT0+e30sIF9vbkVycm9yOiAoKT0+e30sIF9yZW5ld0NhbnZhczogKCk9Pnt9IH07DQoJCQlEZWNyeXB0ZXIuZGVjcnlwdEltZyhubSwgYml0bWFwKTsNCg0KCQkJbGV0IGRzdCA9IG4ucmVwbGFjZUFsbCgnXFwnLCAnLycpLnJlcGxhY2UoJ2ltZycsIGRlc3REaXIpLnJlcGxhY2UoJy5ycGdtdnAnLCAnLnBuZycpOw0KCQkJaW1nLm9ubG9hZCA9ICgpPT57DQoJCQkJbGV0IGNudiA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ2NhbnZhcycpOw0KCQkJCWNudi53aWR0aCA9IGJpdG1hcC5faW1hZ2UubmF0dXJhbFdpZHRoOw0KCQkJCWNudi5oZWlnaHQgPSBiaXRtYXAuX2ltYWdlLm5hdHVyYWxIZWlnaHQ7DQoJCQkJY252LmdldENvbnRleHQoJzJkJykuZHJhd0ltYWdlKGJpdG1hcC5faW1hZ2UsIDAsIDAsIGJpdG1hcC5faW1hZ2UubmF0dXJhbFdpZHRoLCBiaXRtYXAuX2ltYWdlLm5hdHVyYWxIZWlnaHQpOw0KDQoJCQkJbGV0IGRhdGEgPSBjbnYudG9EYXRhVVJMKCkuc3BsaXQoJywnKS5wb3AoKTsNCgkJCQlmcy53cml0ZUZpbGVTeW5jKGRzdCwgZGF0YSwgeyBlbmNvZGluZzogJ2Jhc2U2NCcgfSk7DQoJCQkJbG9nKGDQpNCw0LnQuyAke259INGA0LDRgdGI0LjRhNGA0L7QstCw0L0g0Lgg0YHQutC+0L/QuNGA0L7QstCw0L0g0LIgJHtkc3R9YCk7DQoNCgkJCQlpZihpKzEgPCBmaWxlcy5sZW5ndGgpew0KCQkJCQlkZihpKzEpOw0KCQkJCX1lbHNlew0KCQkJCQlsb2coJ9Ci0LXQutGB0YLRg9GA0L/QsNC6INGB0L7Qt9C00LDQvScpOw0KCQkJCX0NCgkJCX0NCgkJfQ0KDQoJCWRmKDApOw0KCQlwY29uZi50ZXh0dXJlX3BhY2sgPSBwdHA7DQoJfWVsc2V7DQoJCWxvZygn0KLQtdC60YHRgtGD0YDRiyDQvdC1INC30LDRiNC40YTRgNC+0LLQsNC90YsuINCa0L7Qv9C40YDQvtCy0LDQvdC40LUuLi4nKTsNCgkJZmlsZXMuZm9yRWFjaChuPT57DQoJCQlsZXQgbm0gPSBuLnJlcGxhY2UoJ2ltZycsIGRlc3REaXIpOw0KCQkJZnMuY29weUZpbGVTeW5jKG4sIG5tKTsNCgkJCWxvZyhg0KHQutC+0L/QuNGA0L7QstCw0L0g0YTQsNC50Ls6INC40LcgJHtufSDQsiAke25tfWApOw0KCQl9KTsNCgkJbG9nKCfQotC10LrRgdGC0YPRgNC/0LDQuiDRgdC+0LfQtNCw0L0nKTsNCgl9DQp9";

        static void Main(string[] args){
            bool q = args.Length > 1 && args[1] == "-q";
            string dir = "";

            WriteLine(q, "RPGM Patcher 0.4 by nekit270 (https://nekit270.ch)" + Environment.NewLine);

            if(args.Length == 0){
                Console.Write("Укажите путь к папке с игрой: ");
                dir = Console.ReadLine();
            }else{
                dir = args[0];
            }

            string indexPath = GetPath(@$"{dir}\www\index.html");
            string putilsPath = GetPath(dir + @"\www\js\patcherUtils.js", true);
            string corePath = GetPath(@$"{dir}\www\js\rpg_core.js");
            string objPath = GetPath(@$"{dir}\www\js\rpg_objects.js");

            if(indexPath == null || corePath == null || objPath == null){
                WriteLine(q, $"ОШИБКА: Файлы игры в папке {dir} не найдены.{Environment.NewLine}Проверьте правильность пути и повторите попытку.");
                Exit(1);
            }

            Write(q, $"Создание файла: \"{putilsPath}\"... ");
            try{
                File.WriteAllText(putilsPath, Encoding.UTF8.GetString(Convert.FromBase64String(patcherUtilsCode)));
                WriteLine(q, "OK");
            }catch(Exception ue){
                WriteLine(q, Environment.NewLine + "ОШИБКА: " + ue.Message);
                Exit(1);
            }

            Write(q, $"Применение патчей для файла: \"{indexPath}\"... ");
            try{
                File.WriteAllText(indexPath, PatchIndex(File.ReadAllText(indexPath)));
                WriteLine(q, "OK");
            }catch(Exception ie){
                WriteLine(q, Environment.NewLine + "ОШИБКА: " + ie.Message);
                Exit(1);
            }

            Write(q, $"Применение патчей для файла: \"{corePath}\"... ");
            try{
                File.WriteAllText(corePath, PatchCore(File.ReadAllText(corePath)));
                WriteLine(q, "OK");
            }catch(Exception ce){
                WriteLine(q, Environment.NewLine + "ОШИБКА: " + ce.Message);
                Exit(1);
            }

            Write(q, $"Применение патчей для файла: \"{objPath}\"... ");
            try{
                File.WriteAllText(objPath, PatchObjects(File.ReadAllText(objPath)));
                WriteLine(q, "OK");
            }catch(Exception oe){
                WriteLine(q, Environment.NewLine + "ОШИБКА: " + oe.Message);
                Exit(1);
            }
            
            WriteLine(q, Environment.NewLine + "Патчи успешно применены!" + Environment.NewLine + "Нажмите [H] в окне игры для получения справки об использовании функций RPGM Patcher.");
            Exit(0);
        }

        static string PatchIndex(string code){
            code = ToCrLf(code);
            return InsertAfter(code, "</title>\r\n", "<script src=\"js/patcherUtils.js\"></script>\r\n");
        }

        static string PatchCore(string code){
            code = ToCrLf(code);
            code = InsertAfter(
                code,
                "Bitmap.prototype._requestImage = function(url){\r\n",
                "    if(window.pconf.texture_pack && window.pconf.texture_pack != 'none'){\r\n        url = url.replace('img/', `texturepacks/${window.pconf.texture_pack}/`);\r\n    }\r\n    window.loadedTextures.push(url);\r\n"
            );
            code = InsertAfter(
                code,
                "Decrypter.decryptImg = function(url, bitmap) {\r\n    url = this.extToEncryptExt(url);\r\n\r\n    var requestFile = new XMLHttpRequest();\r\n    requestFile.open(\"GET\", url);\r\n    requestFile.responseType = \"arraybuffer\";\r\n    requestFile.send();\r\n\r\n    requestFile.onload = function () {\r\n        if(this.status < Decrypter._xhrOk) {\r\n            var arrayBuffer = Decrypter.decryptArrayBuffer(requestFile.response",
                ", true"
            );
            code = InsertAfter(code, "Decrypter.extToEncryptExt = function(url) {\r\n", "    if(url.endsWith('.png') && window.pconf.texture_pack && window.pconf.texture_pack != 'none') return url;\r\n");
            code = InsertAfter(code, "Decrypter.decryptArrayBuffer = function(arrayBuffer", ", ii");
            code = InsertAfter(code, "Decrypter.decryptArrayBuffer = function(arrayBuffer, ii) {\r\n", "    if(ii && window.pconf.texture_pack && window.pconf.texture_pack != 'none') return arrayBuffer;\r\n");

            return code;
        }

        static string PatchObjects(string code){
            code = ToCrLf(code);

            code = InsertAfter(code, "hp: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "mp: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "tp: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "mhp: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "mmp: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "atk: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "def: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "mat: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            code = InsertAfter(code, "mdf: { get: function() { return ", "(window.godMode&&this.actorId)?Number.MAX_SAFE_INTEGER:");
            
            return code;
        }

        static string ToCrLf(string s){
            return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        static string InsertAfter(string s, string tf, string data){
            int index = s.IndexOf(tf) + tf.Length;
            return s.Insert(index, data);
        }

        static string GetPath(string p)
        {
            if (p.Length == 0) p = ".";
            string path = Path.IsPathRooted(p) ? p : Path.GetFullPath(p);
            if(File.Exists(path) || Directory.Exists(path)) return path;
            return null;
        }

        static string GetPath(string p, bool force)
        {
            if (p.Length == 0) p = ".";
            string path = Path.IsPathRooted(p) ? p : Path.GetFullPath(p);
            if(force || File.Exists(path) || Directory.Exists(path)) return path;
            return null;
        }

        static void Write(bool q, string s){
            if(!q) Console.Write(s);
        }

        static void WriteLine(bool q, string s){
            if(!q) Console.WriteLine(s);
        }

        static void Exit(int code){
            if(Environment.GetCommandLineArgs().Length < 2) Console.ReadLine();
            Environment.Exit(code);
        }
    }
}
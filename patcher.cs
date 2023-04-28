using System;
using System.IO;
using System.Text;

namespace RPGMPatcher{
    class Patcher{
        static string patcherUtilsCode = "Y29uc3QgZnMgPSByZXF1aXJlKCdmcycpOw0KDQppZighZnMuZXhpc3RzU3luYygnd3d3L3RleHR1cmVwYWNrcycpKXsNCglmcy5ta2RpclN5bmMoJ3d3dy90ZXh0dXJlcGFja3MnKTsNCn0NCg0KdHJ5ew0KCXdpbmRvdy5wY29uZiA9IEpTT04ucGFyc2UoZnMucmVhZEZpbGVTeW5jKCdwYXRjaGVyLmpzb24nKSk7DQp9Y2F0Y2goXyl7DQoJd2luZG93LnBjb25mID0gew0KCQl0ZXh0dXJlX3BhY2s6ICdub25lJywNCgkJa2V5czogew0KCQkJUEFVU0U6ICdwJywNCgkJCVNBVkU6ICdzJywNCgkJCVJFU1RBUlQ6ICdyJywNCgkJCUpTX0NPTlNPTEU6ICdqJywNCgkJCUdPRF9NT0RFOiAnZycsDQoJCQlBRERfTU9ORVk6ICdtJywNCgkJCU5PX0NMSVA6ICduJywNCgkJCVNFVF9TUEVFRDogJ2UnLA0KCQkJSEVMUDogJ2gnDQoJCX0NCgl9DQoJZnMud3JpdGVGaWxlU3luYygncGF0Y2hlci5qc29uJywgSlNPTi5zdHJpbmdpZnkocGNvbmYsIG51bGwsIDQpKTsNCn0NCg0Kd2luZG93LmxvYWRlZFRleHR1cmVzID0gW107DQp3aW5kb3cuZ29kTW9kZSA9IGZhbHNlOw0KDQp3aW5kb3cuYWRkRXZlbnRMaXN0ZW5lcigna2V5cHJlc3MnLCBlPT57DQoJc3dpdGNoKGUua2V5KXsNCgkJY2FzZSBwY29uZi5rZXlzLkpTX0NPTlNPTEU6IHsNCgkJCWxldCBpbnAgPSBwcm9tcHQoJ9CS0LLQtdC00LjRgtC1INC60L7QvNCw0L3QtNGDJyk7DQoJCQlpZighaW5wIHx8IGlucC5sZW5ndGggPT0gMCkgcmV0dXJuOw0KCQkJdHJ5ew0KCQkJCWFsZXJ0KGV2YWwoaW5wKSk7DQoJCQl9Y2F0Y2goZSl7DQoJCQkJYWxlcnQoZSk7DQoJCQl9DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5TQVZFOiB7DQoJCQlTY2VuZU1hbmFnZXIucHVzaChTY2VuZV9TYXZlKTsNCgkJCWJyZWFrOw0KCQl9DQoNCgkJY2FzZSBwY29uZi5rZXlzLkdPRF9NT0RFOiB7DQoJCQl3aW5kb3cuZ29kTW9kZSA9IHRydWU7DQoJCQlhbGVydCgn0KDQtdC20LjQvCDQsdC+0LPQsCDQstC60LvRjtGH0LXQvScpOw0KCQkJYnJlYWs7DQoJCX0NCg0KCQljYXNlIHBjb25mLmtleXMuQUREX01PTkVZOiB7DQoJCQkkZ2FtZVBhcnR5Ll9nb2xkID0gTnVtYmVyLk1BWF9TQUZFX0lOVEVHRVI7DQoJCQlhbGVydCgn0JTQtdC90YzQs9C4INC90LDQutGA0YPRh9C10L3RiycpOw0KCQkJYnJlYWs7DQoJCX0NCg0KCQljYXNlIHBjb25mLmtleXMuUEFVU0U6IHsNCgkJCWFsZXJ0KCfQn9CQ0KPQl9CQXG5cbtCd0LDQttC80LjRgtC1INCe0JosINGH0YLQvtCx0Ysg0LLQvtC30L7QsdC90L7QstC40YLRjCcpOw0KCQkJYnJlYWs7DQoJCX0NCg0KCQljYXNlIHBjb25mLmtleXMuUkVTVEFSVDogew0KCQkJbG9jYXRpb24ucmVsb2FkKCk7DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5OT19DTElQOiB7DQoJCQkkZ2FtZVBsYXllci5jYW5QYXNzID0gKCk9PnRydWU7DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5TRVRfU1BFRUQ6IHsNCgkJCWxldCBpbnAgPSBwcm9tcHQoJ9CS0LLQtdC00LjRgtC1INC90L7QstGD0Y4g0YHQutC+0YDQvtGB0YLRjCcsICRnYW1lUGxheWVyLl9tb3ZlU3BlZWQpOw0KCQkJaWYoaW5wICYmICFpc05hTigraW5wKSl7DQoJCQkJJGdhbWVQbGF5ZXIuX21vdmVTcGVlZCA9ICtpbnA7DQoJCQl9DQoJCQlicmVhazsNCgkJfQ0KDQoJCWNhc2UgcGNvbmYua2V5cy5IRUxQOiB7DQoJCQlsZXQgc3RyID0gJ9CT0L7RgNGP0YfQuNC1INC60LvQsNCy0LjRiNC4IFJQR00gUGF0Y2hlclxuXG4nOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLlBBVVNFLnRvVXBwZXJDYXNlKCl9XSDQn9C+0YHRgtCw0LLQuNGC0Ywg0LjQs9GA0YMg0L3QsCDQv9Cw0YPQt9GDXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLlJFU1RBUlQudG9VcHBlckNhc2UoKX1dINCf0LXRgNC10LfQsNC/0YPRgdGC0LjRgtGMINC40LPRgNGDXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLkpTX0NPTlNPTEUudG9VcHBlckNhc2UoKX1dINCe0YLQutGA0YvRgtGMINC60L7QvdGB0L7Qu9GMIEphdmFTY3JpcHRcbmA7DQoJCQlzdHIgKz0gYFske3Bjb25mLmtleXMuU0FWRS50b1VwcGVyQ2FzZSgpfV0g0J7RgtC60YDRi9GC0Ywg0L7QutC90L4g0YHQvtGF0YDQsNC90LXQvdC40Y9cbmA7DQoJCQlzdHIgKz0gYFske3Bjb25mLmtleXMuR09EX01PREUudG9VcHBlckNhc2UoKX1dINCg0LXQttC40Lwg0LHQvtCz0LAgKNC+0YfQtdC90Ywg0LLRi9GB0L7QutC40LUgSFAsINCw0YLQsNC60LAsINC30LDRidC40YLQsCDQuCDRgi7QtC4pXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLkFERF9NT05FWS50b1VwcGVyQ2FzZSgpfV0g0J3QsNC60YDRg9GC0LrQsCDQtNC10L3QtdCzXG5gOw0KCQkJc3RyICs9IGBbJHtwY29uZi5rZXlzLk5PX0NMSVAudG9VcHBlckNhc2UoKX1dIE5vQ2xpcCAo0LLQvtC30LzQvtC20L3QvtGB0YLRjCDQv9GA0L7RhdC+0LTQuNGC0Ywg0YHQutCy0L7Qt9GMINC+0LHRitC10LrRgtGLKVxuYDsNCgkJCXN0ciArPSBgWyR7cGNvbmYua2V5cy5TRVRfU1BFRUQudG9VcHBlckNhc2UoKX1dINCY0LfQvNC10L3QuNGC0Ywg0YHQutC+0YDQvtGB0YLRjCDQuNCz0YDQvtC60LBcbmA7DQoJCQlhbGVydChzdHIpOw0KCQl9DQoJfQ0KfSk7";

        static void Main(string[] args){
            bool q = args.Length > 1 && args[1] == "-q";
            string dir = "";

            WriteLine(q, "RPGM Patcher 0.2 by nekit270 (https://nekit270.ch)" + Environment.NewLine);

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
            return InsertAfter(
                code,
                "Bitmap.prototype._requestImage = function(url){\r\n",
                "if(window.pconf.texture_pack && window.pconf.texture_pack != 'none'){url = url.replace('img/', `texturepacks/${window.pconf.texture_pack}/`);}\r\nwindow.loadedTextures.push(url);\r\n"
            );
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
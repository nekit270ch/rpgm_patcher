using System;
using System.IO;
using System.Text;

namespace RPGMPatcher{
    class Patcher{
        static string patcherUtilsCode = "Y29uc3QgZnMgPSByZXF1aXJlKCdmcycpOw0KDQppZighZnMuZXhpc3RzU3luYygnd3d3L3RleHR1cmVwYWNrcycpKXsNCglmcy5ta2RpclN5bmMoJ3d3dy90ZXh0dXJlcGFja3MnKTsNCn0NCg0KdHJ5ew0KCXdpbmRvdy5wY29uZiA9IEpTT04ucGFyc2UoZnMucmVhZEZpbGVTeW5jKCdwYXRjaGVyLmpzb24nKSk7DQp9Y2F0Y2goXyl7DQoJZnMud3JpdGVGaWxlU3luYygncGF0Y2hlci5qc29uJywgJ3tcclxuICAgIHRleHR1cmVfcGFjazogIm5vbmUiXHJcbn0nKTsNCgl3aW5kb3cucGNvbmYgPSB7IHRleHR1cmVfcGFjazogJ25vbmUnIH07DQp9DQoNCndpbmRvdy5sb2FkZWRUZXh0dXJlcyA9IFtdOw0Kd2luZG93LmdvZE1vZGUgPSBmYWxzZTsNCg0Kd2luZG93Lm9ua2V5cHJlc3MgPSBlPT57DQoJaWYoZS5rZXkgPT0gJ2onKXsNCgkJbGV0IGlucCA9IHByb21wdCgn0JLQstC10LTQuNGC0LUg0LrQvtC80LDQvdC00YMnKTsNCgkJaWYoIWlucCB8fCBpbnAubGVuZ3RoID09IDApIHJldHVybjsNCgkJdHJ5ew0KCQkJYWxlcnQoZXZhbChpbnApKTsNCgkJfWNhdGNoKGUpew0KCQkJYWxlcnQoZSk7DQoJCX0NCgl9ZWxzZSBpZihlLmtleSA9PSAncycpew0KCQlTY2VuZU1hbmFnZXIucHVzaChTY2VuZV9TYXZlKTsNCgl9ZWxzZSBpZihlLmtleSA9PSAnZycpew0KCQl3aW5kb3cuZ29kTW9kZSA9IHRydWU7DQoJCWFsZXJ0KCfQoNC10LbQuNC8INCx0L7Qs9CwINCy0LrQu9GO0YfQtdC9Jyk7DQoJfWVsc2UgaWYoZS5rZXkgPT0gJ20nKXsNCgkJJGdhbWVQYXJ0eS5fZ29sZCA9IE51bWJlci5NQVhfU0FGRV9JTlRFR0VSOw0KCQlhbGVydCgn0JTQtdC90YzQs9C4INC90LDQutGA0YPRh9C10L3RiycpOw0KCX1lbHNlIGlmKGUua2V5ID09ICdwJyl7DQoJCWFsZXJ0KCfQn9CQ0KPQl9CQXG5cbtCd0LDQttC80LjRgtC1INCe0JosINGH0YLQvtCx0Ysg0LLQvtC30L7QsdC90L7QstC40YLRjCcpOw0KCX1lbHNlIGlmKGUua2V5ID09ICdoJyl7DQoJCWFsZXJ0KCfQk9C+0YDRj9GH0LjQtSDQutC70LDQstC40YjQuCBSUEdNIFBhdGNoZXJcblxuW1BdINCf0L7RgdGC0LDQstC40YLRjCDQuNCz0YDRgyDQvdCwINC/0LDRg9C30YNcbltSXSDQn9C10YDQtdC30LDQv9GD0YHRgtC40YLRjCDQuNCz0YDRg1xuW1RdINCe0YLQutGA0YvRgtGMINC+0LrQvdC+INCy0YvQsdC+0YDQsCDRgtC10LrRgdGC0YPRgNC/0LDQutCwXG5bSl0g0J7RgtC60YDRi9GC0Ywg0LrQvtC90YHQvtC70YwgSmF2YVNjcmlwdFxuW1NdINCe0YLQutGA0YvRgtGMINC+0LrQvdC+INGB0L7RhdGA0LDQvdC10L3QuNGPXG5bR10g0KDQtdC20LjQvCDQsdC+0LPQsCAo0L7Rh9C10L3RjCDQstGL0YHQvtC60LjQtSBIUCDQuCDQsNGC0LDQutCwKVxuW01dINCd0LDQutGA0YPRgtC60LAg0LTQtdC90LXQsycpOw0KCX1lbHNlIGlmKGUua2V5ID09ICdiJyl7DQoJCWFsZXJ0KGxvYWRlZFRleHR1cmVzLmpvaW4oJ1xuJykpOw0KCX1lbHNlIGlmKGUua2V5ID09ICdyJyl7DQoJCWxvY2F0aW9uLnJlbG9hZCgpOw0KCX1lbHNlIGlmKGUua2V5ID09ICd0Jyl7DQoJCWxldCBwID0gcHJvbXB0KCfQktCy0LXQtNC40YLQtSDQvdCw0LfQstCw0L3QuNC1INGC0LXQutGB0YLRg9GA0L/QsNC60LAnLCBwY29uZi50ZXh0dXJlX3BhY2spOw0KCQlpZighcCkgcmV0dXJuOw0KCQlwY29uZi50ZXh0dXJlX3BhY2sgPSBwOw0KCQlmcy53cml0ZUZpbGVTeW5jKCdwYXRjaGVyLmpzb24nLCBKU09OLnN0cmluZ2lmeShwY29uZiwgbnVsbCwgNCkpOw0KCQlsb2NhdGlvbi5yZWxvYWQoKTsNCgl9DQp9Ow==";

        static void Main(string[] args){
            bool q = args.Length > 1 && args[1] == "-q";
            string dir = "";

            WriteLine(q, "RPGM Patcher 0.1 by nekit270 (https://nekit270.ch)" + Environment.NewLine);

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
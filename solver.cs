public class Solver {
    public Solver() {}
    public float num = 0;
    private string original;
    char[] chars;

    public void solve(string s) {
        original = s;
        chars = s.ToCharArray();
        separate(chars);
    }

    public float calc(char[] chars) {
        List<string> eqs = new List<string>();

        int start = 0;
        for (int i = 0; i < chars.Count(); i++) {
            string num = "";
            float a;

            if (!float.TryParse(chars[i].ToString(), out a) && chars[i] != ',' && chars[i] != '.' ) {
                if (chars[i] == '-' && (i == 0 || !float.TryParse(chars[i-1].ToString(), out a))) {
                    if (i == chars.Count()-2) { 
                        int x = i+1;
                        while (x < chars.Count() && (float.TryParse(chars[x].ToString(), out a) || chars[i] != ',' && chars[i] != '.')) {
                            num += chars[x].ToString();
                            x++;
                        }
                        
                        // System.Console.Write("negativo: ");
                        // System.Console.WriteLine(num);

                        eqs.Add("-" + num);
                    }
                } else {
                    for (int x = start; x < i; x++) {
                        num += chars[x].ToString();
                    }

                    // System.Console.WriteLine("number added" + num);
                    // System.Console.WriteLine("simboll added" + chars[i].ToString());


                    eqs.Add(num);
                    eqs.Add(chars[i].ToString());

                    start = i+1;
                }
            }

            if (i == chars.Count()-1 ){
                for (int x = start; x <= i; x++) {
                    num += chars[x].ToString();
                }

                // System.Console.WriteLine("num added: " + num);

                eqs.Add(num);
            }
        }

        // foreach(var e in eqs) {
        //     System.Console.Write(e);
        // }
        // System.Console.WriteLine("");

        //0.353+0.23+2+90

        for (int i = 0; i < eqs.Count(); i++) {

            if(eqs[i] == "^") {
                // System.Console.WriteLine(eqs[i-1].ToString() + "^" + eqs[i+1].ToString());
                float a = float.Parse(eqs[i-1].ToString());
                float b = float.Parse(eqs[i+1].ToString());
                eqs.RemoveRange(i-1,3);
                eqs.Insert(i-1, Math.Pow(a,b).ToString());

                // System.Console.WriteLine("pow!: " + Math.Pow(a,b).ToString());
            }
        }

        again:
        for (int i = 0; i < eqs.Count(); i++) {
            if (i != 0) {
                if (eqs[i] == "/" || eqs[i] == "*") {
                    // System.Console.WriteLine(eqs[i-1].ToString() + "* or /" + eqs[i+1].ToString());
                    float a = float.Parse(eqs[i-1].ToString());
                    float b = float.Parse(eqs[i+1].ToString());

                    string num = (eqs[i] == "/" ? a/b : a*b).ToString();

                    eqs.RemoveRange(i-1,3);
                    eqs.Insert(i-1, num);

                    goto again;
                }
            }
        }


        float result = 0;
        for (int i = 0; i<eqs.Count(); i++) {
            float a = 1;

            var parse = float.TryParse(eqs[i], out a);

            //System.Console.WriteLine("n: " + eqs[i]);

            if (i == 0 && parse == true) {
                result += float.Parse(eqs[i]);
            }
            
            if (parse == false) {
                result += (eqs[i] == "+") ? float.Parse(eqs[i+1]) : -float.Parse(eqs[i+1]);
            } 
        }
        // System.Console.WriteLine();
        //System.Console.WriteLine("Result: " + result + "; ");
        
        return result;
    }

    public int p(char[] _list) {
        for(int i = 0; i < _list.Count(); i++) {
            if (_list[i] == '(') {
                return i;
            }
        }
        return 0;
    }

    public void separate(char[] l) {
        var parentheses = p(l);
        var s = new Solver();
        int skip = 0;
        float result = 0;

        if (parentheses != 0) {
            for (int i = parentheses+1; i < l.Count(); i++) {
                if (l[i] == '(') {
                    skip += 1;
                }

                if (l[i] == ')'&& skip == 0) {
                    List<char> l2 = new List<char>();
                    for (int x = parentheses+1; x < i; x++) {
                        // System.Console.WriteLine(l2.ToArray());
                        l2.Add(l[x]);
                    }

                    //System.Console.WriteLine(l2.ToArray());

                    if (p(l2.ToArray()) != 0) {
                        separate(l2.ToArray());
                    } else {
                        string replace = "";
                        foreach(var y in l2) { replace += y;}
                        original = original.Replace("(" + replace + ")", s.calc(l2.ToArray()).ToString());
                        // System.Console.WriteLine(original);
                        solve(original);
                    }
                    break;
                }

                if (skip != 0 && l[i] == ')') {
                    skip --;
                }
            }
        } else {
            result = calc(original.ToArray());
            if (result != 0) { System.Console.WriteLine("Result: " + result); }
        }
    }
}
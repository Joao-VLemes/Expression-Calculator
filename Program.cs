

var c = new Solver();

string eq;

System.Console.WriteLine("Calculator: ");
eq = Console.ReadLine().Replace(" ", "");
if(eq=="") System.Environment.Exit(1);

c.solve(eq);

Console.ReadLine();
// int p = 0;
// for (int i = 0; i < chars.Count(); i++) {
//     if (chars[i] == '(') p = i;

//     if (chars[i] == ')') {
//         for (int x = p+1; x < i; x++) {
//             parentheses += chars[x];
//         }
//     }
// }

// System.Console.WriteLine(parentheses);

// eq = eq.Replace("(" + parentheses + ")", c.calc(parentheses.ToCharArray()).ToString());

// System.Console.WriteLine("Answer: " + c.calc(eq.ToCharArray()));
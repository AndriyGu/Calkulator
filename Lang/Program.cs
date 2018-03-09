using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    
    class Env // среда это то место где хранится контекст.
    {
        public Dictionary<string, int> variables;                           // карта, "куча" HEAP
        public Env()                                                        // конструктор который карту        
        {
            variables = new Dictionary<string, int>();                      
        }
    }

    interface Exp
    {
        // @env - контекст который необходим для вычесления конкретного объекта, унаследованого от interface Exp
        int eval(Env env);  // evaluate - вычислить, оценить
    }
    
    class Const : Exp // 
    {
        int c;
        public Const(int c) { this.c = c; }
        public int eval(Env env)
        {
            return c;
        }
        // LI r1, $c  
    }

    class Var : Exp
    {
        string name;
        public Var(string name) { this.name = name; }
        public int eval(Env env)
        {
            int value;
            if (!env.variables.TryGetValue(name, out value))
                throw new Exception("no corresponding variable found!");
            return value;
        }
        // compiled_varialbes<name, addr_t> cv;
        // new LW( r1, $cv[name]);
    }
    class Add : Exp
    {
        Exp left;
        Exp right;
        public Add(Exp left, Exp right) { this.left = left; this.right = right; }
        public int eval(Env env)
        {
            return left.eval(env) + right.eval(env) ;
        }
    }
    class Sub : Exp
    {
        Exp left;
        Exp right;
        public Sub(Exp left, Exp right) { this.left = left; this.right = right; }
        public int eval(Env env)
        {
            return left.eval(env) - right.eval(env);
        }
    }
    // thdfdfhdf fghdfgyhdf 
    
    interface Stmt
    {
       void eval(Env env);
    }

    class Skip : Stmt
    {
        public Skip() { }
        public void eval(Env env)
        {
            return;
        }
    }
    class Assign : Stmt  // оператор присваивания
    {
        string name;
        Exp exp;  
        // a = b + 10  new Asign("a", new Add(new Var("b"), new Const(10)))
        public Assign(string name, Exp exp) { this.name = name; this.exp = exp; }
        public void eval(Env env)
        {
            env.variables[name] = exp.eval(env);
        }
    }
    class Seq : Stmt // опрaтор последовательности
    {
        Stmt left;
        Stmt right;
        public Seq(Stmt left, Stmt right) { this.left = left; this.right = right; }
        public Seq(Stmt[] stmts) { }
        public void eval(Env env)
        {
            left.eval(env);
            right.eval(env);
        }
    }
    class If : Stmt
    {
        Exp exp;
        Stmt left;
        Stmt right;
        public If(Exp exp, Stmt left, Stmt right) { this.exp = exp; this.left = left; this.right = right; }
        public void eval(Env env)
        {
            if (exp.eval(env) != 0)
                left.eval(env);
            else
                right.eval(env);
        }
    }
    class While : Stmt
    {
        Exp exp;
        Stmt stmt;
        public While(Exp exp, Stmt stmt)
        {
            this.exp = exp; this.stmt = stmt;
        }
        public void eval(Env env)
        {
            while (exp.eval(env) != 0)
                stmt.eval(env);
        }
    }

    

    class Program
    {
        static Const constant(int c) { return new Const(c); }
        static Var var(string name) { return new Var(name); }
        static Sub sub(Exp left, Exp right) { return new Sub(left, right); }
        static Add add(Exp left, Exp right) { return new Add(left, right); }
        static void eval(Stmt s)
        {
            Env env = new Env();
            s.eval(env);
            Console.WriteLine("---");
            foreach(var kv in env.variables)
            {
                Console.WriteLine("{0} = {1}", kv.Key, kv.Value);
            }
        }

        static void Main(string[] args)
        {

            Token token = new Token(); 
            List<Token> tokens = token.makePars("10+20;a=23+59;");////>=

            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                if (tokens[i] == null)
                    tokens.RemoveAt(i);
            }

            Token.printingListToken(tokens);

           
            int a = 20;
            Exp exp = parseExp(tokens, 0, null); // Add(Var('a'), Add(Const(10), Var(b)))

            int b = exp.eval(new Env());

            int ipo = 20;
        }


        public static Exp parseExp(List<Token> t, int i, Exp last_seen)
        {
            if (t[i].typeGet() == TokenTypeEnum.CONST)
            {
                if (last_seen != null) { throw new Exception(); }

                last_seen = new Const(Int32.Parse(t[i].tokenValueGet()));
                if (i + 1 == t.Count) { return last_seen; }
                else { return parseExp(t, i + 1, last_seen); }
            }
            else if (t[i].typeGet() == TokenTypeEnum.VAR)
            {
                if (last_seen != null)
                    throw new Exception();

                last_seen = new Var(t[i].tokenValueGet());
                if (i + 1 == t.Count) { return last_seen; }
                else { return parseExp(t, i + 1, last_seen); }
            }
            else if (t[i].typeGet() == TokenTypeEnum.PLUS)
            {
                if (i + 1 == t.Count)
                    throw new Exception();
                return new Add(last_seen, parseExp(t, i + 1, null));
            }
            else if (t[i].typeGet() == TokenTypeEnum.SUB)
            {
                if (i + 1 == t.Count)
                    throw new Exception();
                return new Sub(last_seen, parseExp(t, i + 1, null));
            }

            

            return null;
        }

        public static Stmt parseStmt(List<Token> t, int i, Exp last_seen)
        {
            //else if (t[i].typeGet() == TokenTypeEnum.ASSIGN)
            //{
            //    if (i + 1 == t.Count)
            //        throw new Exception();
            //    return new Assign(last_seen, parseExp(t, i + 1, null));
            //}

            return null;
        }


        }
    
}

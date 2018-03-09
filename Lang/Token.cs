using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading.Tasks;

namespace Lang
{enum TokenTypeEnum { VAR, CONST, ASSIGN, PLUS, SUB, KEY_WORD, SEQ, DIV, L_BR, R_BR, L_CR_BR, R_CR_BR,
                        IF, 

    };
    class Token
    {
        TokenTypeEnum type;
        String token;

        public Token() { }

        public Token(TokenTypeEnum type) { this.type = type; }// изза херни с масивом масивов

        public Token(TokenTypeEnum type, string token)
        {
            this.type = type;
            this.token = token;
        }

      

        public void typeSet(TokenTypeEnum value) { this.type = (TokenTypeEnum)value; }
        public TokenTypeEnum typeGet() { return this.type; }

        public void tokenValueSet(String value) { this.token = value; }
        public String tokenValueGet() { return this.token; }

        // массив разрешенных символов после числа (возможно станут кейвордами)


        public static List<Token> allowedWords = new List<Token> ()
        {
              
              new Token(TokenTypeEnum.KEY_WORD, "WHILE"),
              new Token(TokenTypeEnum.KEY_WORD, "IF"),
             
        };



        public static List<Token> splitSymbols = new List<Token>()
        {
              new Token(TokenTypeEnum.ASSIGN, "="),
              new Token(TokenTypeEnum.PLUS, "+"),
              new Token(TokenTypeEnum.SUB, "-"),
              new Token(TokenTypeEnum.DIV, "/"),
              new Token(TokenTypeEnum.SEQ, ";"),
              //L_BR, R_BR, L_CR_BR, R_CR_BR
              new Token(TokenTypeEnum.L_BR, "("),
              new Token(TokenTypeEnum.R_BR, ")"),
              new Token(TokenTypeEnum.L_CR_BR, "{"),
              new Token(TokenTypeEnum.R_CR_BR, "}"),
              new Token(TokenTypeEnum.R_CR_BR, ">="),
              new Token(TokenTypeEnum.R_CR_BR, ">"),
        };
        List<Token> tokenList = new List<Token>();

        public void makeLexems(String line, ref List<Token> tokenList)
        {
            /*
             *Может быть ошибка если
             *   на первой позиции минус
             *   строка не закрыта ";"  (а = 10 + 20)
             *
            */

            string tempWord="";
          
          for(int i = 0; i<line.Length; i++)
            {
                string adapter = ""+line[i];
                string tempSymbol = "" + line[i];
                foreach(Token tempToken in splitSymbols)
                {
                    // если встречаем разделитель то то что слева должно быть "распознанно"
                    if (tempSymbol.Equals(tempToken.tokenValueGet()))
                    {
                        tokenList.Add(createToken(tempWord));


                        tokenList.Add(tempToken);
                        tempWord = "";
                        adapter = "";
                        break;
                    }
                    
                }
                tempWord = tempWord + adapter;
            }
            


        }

        public Token createToken(string line)
        {
            Token resultToken=null;
            if (isKEY_WORD(line))
            {
                foreach (Token token in allowedWords)
                {
                    if (token.tokenValueGet().Equals(line)) { resultToken = token; break; }
                }
            }
            else if (isVAR(line)) { Token token = new Token(TokenTypeEnum.VAR, line); resultToken = token;}
            else if (isCONST(line)) { Token token = new Token(TokenTypeEnum.CONST, line); resultToken = token; }
            else { Console.WriteLine("Erorrrrr"); }
            return resultToken;

        }

        public string removeSpaces(string inputString)
        {
            inputString = inputString.Replace("  ", string.Empty);
            inputString = inputString.Trim().Replace(" ", string.Empty);

            return inputString;
        }

        public List<Token> makePars(string inputString)
        {
           
           Token token = new Token();
            
            token.makeLexems(removeSpaces(inputString), ref tokenList);
            return tokenList;
        }

     
        public int findMaxLengthWord(List<Token>  allowedSymbols)
        {
            int max = 0;
            foreach (Token temp in  allowedSymbols)
            {
                if (temp.tokenValueGet().Length > max) { max = temp.tokenValueGet().Length; }
            }
            return max;
        }

        public static bool isVAR(String line)
        {
            Regex rgx = new Regex("^[a-zA-Z]+[_]*[a-zA-Z]*$");
            return rgx.IsMatch(line);
        }

        public static bool isKEY_WORD(String line)
        {
            bool result = false;
            foreach (Token token in allowedWords)
            {
                if (token.tokenValueGet().Equals(line)) { result =  true; break; }
            }

            return result;
        }

        public static bool isCONST(String line)
        {
            Regex rgx = new Regex("^[0-9]+[.]*[0-9]*$");
            return rgx.IsMatch(line);
        }

        public static void printingListToken(List<Token> tokens)
        {
            foreach (Token token in tokens) { Console.WriteLine(token.tokenValueGet() + "  " + token.typeGet()); }
        }
    }
}

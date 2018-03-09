using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{

    class StatementParseType
    {
        public Token t;



        public StatementParseType(Token t)
        {
            this.t = t;
        }
    };

    class Interpreter
    {





        StatementParseType[][] statement_parse_rules =
     {
                new StatementParseType[] {new StatementParseType(new Token(TokenTypeEnum.VAR)) }

        };
//                new StatementParseType[] {"VAR"},
//  (PUNCT '='),
//  exp,
// },
 
// {(KEYWORD 'if'),
//  (KEYWORD 'if'),
//  (PUNCT '('),
//  exp,
//  (PUNCT ')'),
//  (PUNCT '{'),
//  statement(PUNCT '}')},


// {(KEYWORD 'while'), 
//  (PUNCT '('),
//  exp,
//  (PUNCT ')'), 
//  (PUNCT '{'),
//  statement,
//  (PUNCT '}')
// }
//}


////exp =     CONST
//// | VAR
//// | exp (PUNCT '+') exp
//// | exp (PUNCT '-') exp


 
//ExprParseType exp_parse_rules[][] = {
// { CONST },
// { VAR, STRING },
// { REC, (PUNCT '+'), REC },
//}
 
//class ExprParseType
//{
//}

//class StatementParseType
//{
//    bool not_equals(Token[] token, int offset)
//    {
//        if (token.type() == exp)
//        {
//            return parse_exp(Token[] token, int offset);
//        }
//    }
//}

////VAR b, EQ, CONST 100, VAR a, EQ, 10, KEYWORD while, LPAREN, VAR a, NE, CONST 0, RPAREN, LFPAREN, VAR a, EQ,
////VAR a, SUB, CONST 1, VAR b, ...     

//void parse(Token[] tokens)
//{
//    int offset = 0;
//    bool parsed;

//    for (int i = 0; i < statement_parse_rules.length(); ++i)
//    {
//        for (int j = 0; j < statement_parse_rules[i].legth(); ++j)
//        {
//            parsed = true;
//            if (statement_parse_rules[i][j].not_equals(tokens[], j + offset))
//            {
//                parsed = false;
//                break;
//            }
//        }
    
} }



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingProcess.Service
{
    public class Parser
    {
        private ParserLocation location;

        public Parser(string input)
        {
            location = new ParserLocation(input);
        }

        public GroupService ParseGroup()
        {
            if (UnignoredChar() != '{')
                throw new Exception("'{' expected");

            var contents = ContentParsers();

            if (UnignoredChar() != '}')
                throw new Exception("'}' expected");

            return new GroupService(contents);
        }

        List<IContentParser> ContentParsers()
        {
            var list = new List<IContentParser>();
            while (CurrentCharacter != '}')
            {
                list.Add(ParseContent());

                if (CurrentCharacter == '}')
                    break;

                if (UnignoredChar() != ',')
                    throw new Exception("',' or '}' expected");
            }
            return list;
        }

        IContentParser ParseContent()
        {
            if (CurrentCharacter == '{')
                return ParseGroup();
            if (CurrentCharacter == '<')
                return ParseGarbage();
            return null;
        }

        IContentParser ParseGarbage()
        {
            if (UnignoredChar() != '<')
                throw new Exception("'<' expected");

            var garbage = new List<char>();

            while (true)
            {
                var next = UnignoredChar();
                if (next == null)
                    throw new Exception("End of stream");
                if (next == '>')
                    return new Garbage(new String(garbage.ToArray()));
                garbage.Add(next.Value);
            }
        }

        char? UnignoredChar()
        {
            var c = NextChar();
            if (c == '!')
            {
                NextChar();
                return UnignoredChar();
            }
            return c;
        }

        char? CurrentCharacter => location.Current;

        char? NextChar()
        {
            var c = location.Current;
            location = location.MoveToRight();
            return c;
        }
    }
    public class Garbage : IContentParser
    {
        private readonly string _garbage;

        public Garbage(string garbage)
        {
            _garbage = garbage;
        }


        public int GetScore(int s)
        {
            return 0;
        }
    }
}

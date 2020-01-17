using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
namespace Automation.Common.Utils
{
    public class HttpUtility
    {
        public static string UrlDecode(string str)
        {
            return UrlDecode(str, Encoding.UTF8);
        }

        private static char[] GetChars(MemoryStream b, Encoding e)
        {
            return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
        }

        private static int GetInt(byte b)
        {
            char c = (char)b;
            if (c >= '0' && c <= '9')
                return c - '0';

            if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;

            return -1;
        }

        private static int GetChar(byte[] bytes, int offset, int length)
        {
            int value = 0;
            int end = length + offset;
            for (int i = offset; i < end; i++)
            {
                int current = GetInt(bytes[i]);
                if (current == -1)
                    return -1;
                value = (value << 4) + current;
            }

            return value;
        }

        private static int GetChar(string str, int offset, int length)
        {
            int val = 0;
            int end = length + offset;
            for (int i = offset; i < end; i++)
            {
                char c = str[i];
                if (c > 127)
                    return -1;

                int current = GetInt((byte)c);
                if (current == -1)
                    return -1;
                val = (val << 4) + current;
            }

            return val;
        }

        public static string UrlDecode(string s, Encoding e)
        {
            if (null == s)
                return null;

            if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
                return s;

            if (e == null)
                e = Encoding.UTF8;

            StringBuilder output = new StringBuilder();
            long len = s.Length;
            MemoryStream bytes = new MemoryStream();
            int xchar;

            for (int i = 0; i < len; i++)
            {
                if (s[i] == '%' && i + 2 < len && s[i + 1] != '%')
                {
                    if (s[i + 1] == 'u' && i + 5 < len)
                    {
                        if (bytes.Length > 0)
                        {
                            output.Append(GetChars(bytes, e));
                            bytes.SetLength(0);
                        }

                        xchar = GetChar(s, i + 2, 4);
                        if (xchar != -1)
                        {
                            output.Append((char)xchar);
                            i += 5;
                        }
                        else
                        {
                            output.Append('%');
                        }
                    }
                    else if ((xchar = GetChar(s, i + 1, 2)) != -1)
                    {
                        bytes.WriteByte((byte)xchar);
                        i += 2;
                    }
                    else
                    {
                        output.Append('%');
                    }
                    continue;
                }

                if (bytes.Length > 0)
                {
                    output.Append(GetChars(bytes, e));
                    bytes.SetLength(0);
                }

                if (s[i] == '+')
                {
                    output.Append(' ');
                }
                else
                {
                    output.Append(s[i]);
                }
            }

            if (bytes.Length > 0)
            {
                output.Append(GetChars(bytes, e));
            }

            bytes = null;
            return output.ToString();
        }

        public static string UrlDecode(byte[] bytes, Encoding e)
        {
            if (bytes == null)
                return null;

            return UrlDecode(bytes, 0, bytes.Length, e);
        }

        public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
        {
            if (bytes == null)
                return null;
            if (count == 0)
                return string.Empty;

            if (bytes == null)
                throw new ArgumentNullException("bytes");

            if (offset < 0 || offset > bytes.Length)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || offset + count > bytes.Length)
                throw new ArgumentOutOfRangeException("count");

            var output = new StringBuilder();
            var acc = new MemoryStream();

            int end = count + offset;
            int xchar;
            for (int i = offset; i < end; i++)
            {
                if (bytes[i] == '%' && i + 2 < count && bytes[i + 1] != '%')
                {
                    if (bytes[i + 1] == (byte)'u' && i + 5 < end)
                    {
                        if (acc.Length > 0)
                        {
                            output.Append(GetChars(acc, e));
                            acc.SetLength(0);
                        }
                        xchar = GetChar(bytes, i + 2, 4);
                        if (xchar != -1)
                        {
                            output.Append((char)xchar);
                            i += 5;
                            continue;
                        }
                    }
                    else if ((xchar = GetChar(bytes, i + 1, 2)) != -1)
                    {
                        acc.WriteByte((byte)xchar);
                        i += 2;
                        continue;
                    }
                }

                if (acc.Length > 0)
                {
                    output.Append(GetChars(acc, e));
                    acc.SetLength(0);
                }

                if (bytes[i] == '+')
                {
                    output.Append(' ');
                }
                else
                {
                    output.Append((char)bytes[i]);
                }
            }

            if (acc.Length > 0)
            {
                output.Append(GetChars(acc, e));
            }

            acc = null;
            return output.ToString();
        }


        public static NameValueCollection ParseFragmentString(string fragment)
        {
            return ParseFragmentString(fragment, Encoding.UTF8);
        }

        public static NameValueCollection ParseFragmentString(string query, Encoding encoding)
        {
            return ParseString(query, encoding, '#');
        }

        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            return ParseString(query, encoding, '?');
        }

        public static bool IsUriValid(string uriToValidate)
        {
            Uri uriResult;
            return Uri.TryCreate(uriToValidate, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private static NameValueCollection ParseString(string query, Encoding encoding, char identifier)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (query.Length == 0 || (query.Length == 1 && query[0] == identifier))
                return new NameValueCollection();
            if (query[0] == '?')
                query = query.Substring(1);

            var result = new NameValueCollection();
            ParseQueryString(query, encoding, result);
            return result;

        }

        internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            int namePos = 0;
            while (namePos <= query.Length)
            {
                int valuePos = -1, valueEnd = -1;
                for (int q = namePos; q < query.Length; q++)
                {
                    if (valuePos == -1 && query[q] == '=')
                    {
                        valuePos = q + 1;
                    }
                    else if (query[q] == '&')
                    {
                        valueEnd = q;
                        break;
                    }
                }

                string name, value;
                if (valuePos == -1)
                {
                    name = null;
                    valuePos = namePos;
                }
                else
                {
                    name = UrlDecode(query.Substring(namePos, valuePos - namePos - 1), encoding);
                }
                if (valueEnd < 0)
                {
                    namePos = -1;
                    valueEnd = query.Length;
                }
                else
                {
                    namePos = valueEnd + 1;
                }
                value = UrlDecode(query.Substring(valuePos, valueEnd - valuePos), encoding);

                result.Add(name, value);
                if (namePos == -1) break;
            }
        }
    }
}

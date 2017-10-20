using System;
using System.Collections.Generic;
using System.Text;

namespace SmileBotCore
{
    class makeitfancy
    {
        public string _makeitfancy(string metin)
        {
            string output = "";
            foreach(char c in metin)
            {
                if (c == 'a') output += " :regional_indicator_a:"; 
                if (c == 'b') output += " :regional_indicator_b:";
                if (c == 'c') output += " :regional_indicator_c:";
                if (c == 'd') output += " :regional_indicator_d:";
                if (c == 'e') output += " :regional_indicator_e:";
                if (c == 'f') output += " :regional_indicator_f:";
                if (c == 'g') output += " :regional_indicator_g:";
                if (c == 'h') output += " :regional_indicator_h:";
                if (c == 'i') output += " :regional_indicator_i:";
                if (c == 'j') output += " :regional_indicator_j:";
                if (c == 'k') output += " :regional_indicator_k:";
                if (c == 'l') output += " :regional_indicator_l:";
                if (c == 'm') output += " :regional_indicator_m:";
                if (c == 'n') output += " :regional_indicator_n:";
                if (c == 'o') output += " :regional_indicator_o:";
                if (c == 'p') output += " :regional_indicator_p:";
                if (c == 'r') output += " :regional_indicator_r:";
                if (c == 's') output += " :regional_indicator_s:";
                if (c == 't') output += " :regional_indicator_t:";
                if (c == 'u') output += " :regional_indicator_u:";
                if (c == 'v') output += " :regional_indicator_v:";
                if (c == 'y') output += " :regional_indicator_y:";
                if (c == 'z') output += " :regional_indicator_z:";
                if (c == 'x') output += " :regional_indicator_x:";
                if (c == 'w') output += " :regional_indicator_w:";
                if (c == 'q') output += " :regional_indicator_q:";
                if (c == ' ') output += "   ";
                if (c == 'ı') output += " :regional_indicator_i:";
                if (c == 'ş') output += " :regional_indicator_s:";
                if (c == 'ğ') output += " :regional_indicator_g:";
                if (c == 'ü') output += " :regional_indicator_u:";
                if (c == 'ö') output += " :regional_indicator_o:";
                if (c == 'ç') output += " :regional_indicator_c:";
            }
            return output;
        }
    }
}

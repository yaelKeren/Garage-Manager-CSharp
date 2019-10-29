using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public class eCarEnums
    {
        public enum kindOfColor
        {
            Undefined,
            Yellow,
            White,
            Red,
            Black
        }

        public enum NumberOfDoors
        {
            Undefined,
            Two,
            Three,
            Four,
            Five
        }

        //this class convert from enum kindOfColor/NumberOfDoors to string
        public class CarEnumsToText
        {

            // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
            public static string AsText(kindOfColor i_eColor)
            {
                string enumDescripton = "null";
                switch (i_eColor)
                {
                    case kindOfColor.Yellow:
                    {
                        enumDescripton = "Yellow";
                        break;
                    }

                    case kindOfColor.White:
                    {
                            enumDescripton = "White";
                        break;
                    }

                    case kindOfColor.Red:
                    {
                            enumDescripton = "Red";
                        break;
                    }

                    case kindOfColor.Black:
                    {
                        enumDescripton = "Black";
                        break;
                    }
                }

                return enumDescripton;
            }


            // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
            public static string AsText(NumberOfDoors i_eNumberOfDoors)
            {
                string enumDescripton = "null";
                switch (i_eNumberOfDoors)
                {
                    case NumberOfDoors.Two:
                    {
                        enumDescripton = "Two";
                        break;
                    }

                    case NumberOfDoors.Three:
                    {
                        enumDescripton = "Three";
                        break;
                    }

                    case NumberOfDoors.Four:
                    {
                        enumDescripton = "Four";
                        break;
                    }

                    case NumberOfDoors.Five:
                    {
                        enumDescripton = "Five";
                        break;
                    }
                }

                return enumDescripton;
            }

        }

    }
}


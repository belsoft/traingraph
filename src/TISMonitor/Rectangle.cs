namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public static readonly Rectangle Empty;
        private int x;
        private int y;
        private int width;
        private int height;
        public Rectangle(int x2, int y2, int width2, int height2)
        {
            this.x = x2;
            this.y = y2;
            this.width = width2;
            this.height = height2;
        }

        public static Rectangle FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public Point Location
        {
            get
            {
                return new Point((double) this.X, (double) this.Y);
            }
            set
            {
                this.X = (int) value.get_X();
                this.Y = (int) value.get_Y();
            }
        }
        public System.Windows.Size Size
        {
            get
            {
                return new System.Windows.Size((double) this.Width, (double) this.Height);
            }
            set
            {
                this.Width = (int) value.get_Width();
                this.Height = (int) value.get_Height();
            }
        }
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        public int Left
        {
            get
            {
                return this.X;
            }
        }
        public int Top
        {
            get
            {
                return this.Y;
            }
        }
        public int Right
        {
            get
            {
                return (this.X + this.Width);
            }
        }
        public int Bottom
        {
            get
            {
                return (this.Y + this.Height);
            }
        }
        public bool IsEmpty
        {
            get
            {
                return ((((this.height == 0) && (this.width == 0)) && (this.x == 0)) && (this.y == 0));
            }
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }
            Rectangle rectangle = (Rectangle) obj;
            return ((((rectangle.X == this.X) && (rectangle.Y == this.Y)) && (rectangle.Width == this.Width)) && (rectangle.Height == this.Height));
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return ((((left.X == right.X) && (left.Y == right.Y)) && (left.Width == right.Width)) && (left.Height == right.Height));
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public bool Contains(int x, int y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        public bool Contains(Rectangle rect)
        {
            return ((((this.X <= rect.X) && ((rect.X + rect.Width) <= (this.X + this.Width))) && (this.Y <= rect.Y)) && ((rect.Y + rect.Height) <= (this.Y + this.Height)));
        }

        public override int GetHashCode()
        {
            return (((this.X ^ ((this.Y << 13) | (this.Y >> 0x13))) ^ ((this.Width << 0x1a) | (this.Width >> 6))) ^ ((this.Height << 7) | (this.Height >> 0x19)));
        }

        public void Inflate(int width, int height)
        {
            this.X -= width;
            this.Y -= height;
            this.Width += 2 * width;
            this.Height += 2 * height;
        }

        public static Rectangle Inflate(Rectangle rect, int x, int y)
        {
            Rectangle rectangle = rect;
            rectangle.Inflate(x, y);
            return rectangle;
        }

        public void Intersect(Rectangle rect)
        {
            Rectangle rectangle = Intersect(rect, this);
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
        }

        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            int num = Math.Max(a.X, b.X);
            int num2 = Math.Min((int) (a.X + a.Width), (int) (b.X + b.Width));
            int num3 = Math.Max(a.Y, b.Y);
            int num4 = Math.Min((int) (a.Y + a.Height), (int) (b.Y + b.Height));
            if ((num2 >= num) && (num4 >= num3))
            {
                return new Rectangle(num, num3, num2 - num, num4 - num3);
            }
            return Empty;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return ((((rect.X < (this.X + this.Width)) && (this.X < (rect.X + rect.Width))) && (rect.Y < (this.Y + this.Height))) && (this.Y < (rect.Y + rect.Height)));
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            int num = Math.Min(a.X, b.X);
            int num2 = Math.Max((int) (a.X + a.Width), (int) (b.X + b.Width));
            int num3 = Math.Min(a.Y, b.Y);
            int num4 = Math.Max((int) (a.Y + a.Height), (int) (b.Y + b.Height));
            return new Rectangle(num, num3, num2 - num, num4 - num3);
        }

        public void Offset(int x, int y)
        {
            this.X += x;
            this.Y += y;
        }

        public override string ToString()
        {
            return ("{X=" + this.X.ToString() + ",Y=" + this.Y.ToString() + ",Width=" + this.Width.ToString() + ",Height=" + this.Height.ToString() + "}");
        }

        static Rectangle()
        {
            Empty = new Rectangle();
        }
    }
}


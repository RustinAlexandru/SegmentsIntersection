using System;

namespace SegmentsIntersection
{
   
    class Point
    {
        private float x;
        private float y;

        public Point()
        { 
            this.x = 0;
            this.y = 0;
        }

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float X
        {
            get
            {
                return x;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
        }

        public override string ToString()
        {
            return "X: " + this.x + "  Y: " + this.y;
        }

      
    }

    class Line
    {
        private Point startPoint;
        private Point endPoint;

        public Line()
        {
            startPoint = new Point();
            endPoint = new Point();
        }

        public Line(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public Line(float x1, float y1, float x2, float y2) : 
          this(new Point(x1,y1), new Point(x2,y2))
        {}


        public Point StartPoint
        {
            get
            {
                return startPoint;
            }
        }

        public Point EndPoint
        {
            get
            {
                return endPoint;
            }
        } 


    }


    class IntersectionTest
    {
        public IntersectionTest()
        {
            
        }

        public bool IsPointOnLine(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        public float Orientation(Point p, Point q, Point r)
        {
            float rez = (q.Y - p.Y) * (r.X - q.X)
                -(q.X - p.X) * (r.Y - q.Y);
            if (rez == 0)
                return 0;
            if (rez > 0) {
                return 1;
            } else {
                return -1;
            }
        }

        public bool lineSegmentTouchesOrCrossesLine(Line a, Line b)
        {
            float o1 = Orientation(b.StartPoint, b.EndPoint, a.StartPoint), 
                o2 = Orientation(b.StartPoint, b.EndPoint, a.EndPoint),
                o3 = Orientation(a.StartPoint, a.EndPoint, b.StartPoint), 
                o4 = Orientation(a.StartPoint, a.EndPoint, b.EndPoint);

            if (((o1 > 0 && o2 < 0) || (o1 < 0 && o2 > 0))
                && ((o3 > 0 && o4 < 0) || (o3 < 0 && o4 > 0)))
                return true;

            else if (o1 == 0 && IsPointOnLine(b.StartPoint, a.StartPoint, b.EndPoint))
                return true;
            else if (o2 == 0 && IsPointOnLine(b.StartPoint, a.EndPoint, b.EndPoint))
                return true;
            else if (o3 == 0 && IsPointOnLine(a.StartPoint, b.StartPoint, a.EndPoint))
                return true;
            else if (o4 == 0 && IsPointOnLine(a.StartPoint, b.EndPoint, a.EndPoint))
                return true;

            else
                return false;

        }

        public  bool ColinearSegments(Line a, Line b) {
            Point r = SubtractPoints(a.EndPoint, a.StartPoint);
            Point s = SubtractPoints(b.EndPoint, b.StartPoint);

            float uNumerator = CrossProduct(SubtractPoints(b.StartPoint,a.StartPoint),r);
            float denominator = CrossProduct(r,s);

            if(uNumerator == 0 && denominator == 0) return true;
            return false;

        }

        public  Point SubtractPoints(Point p , Point q) {
            return new Point(p.X - q.X, p.Y - q.Y);
        }

        public  float CrossProduct(Point p, Point q) {
            return p.X * q.Y - p.Y * q.X;
        }

        public bool findintersection(Line a, Line b)
        {
            float miu, lambda;

            miu = ((b.StartPoint.X - a.StartPoint.X)*(a.EndPoint.Y - a.StartPoint.Y) - (b.StartPoint.Y - a.StartPoint.Y)*(a.EndPoint.X - a.StartPoint.X))
                /((a.EndPoint.Y - b.StartPoint.Y)*(a.EndPoint.X - a.StartPoint.X) - (b.EndPoint.X - b.StartPoint.X)*(a.EndPoint.Y - a.StartPoint.Y));
            lambda = ((b.StartPoint.X - a.StartPoint.X)*(a.EndPoint.Y - a.StartPoint.Y) 
                - miu*(b.EndPoint.X - b.StartPoint.X)*(a.EndPoint.Y - a.StartPoint.Y)) / ((a.EndPoint.X - a.StartPoint.X)*(a.EndPoint.Y - a.StartPoint.Y));

            if((lambda >= 0 && lambda <=1) && (miu >= 0 && miu <=1))
                return true;
            else
                return false;

        }

        public bool DoIntersect(Line a, Line b)
        {
                Point A = a.StartPoint;
                Point B = a.EndPoint;
                Point C = b.StartPoint;
                Point D = b.EndPoint;

                Point CmP = new Point(C.X - A.X, C.Y - A.Y);
                Point r = new Point(B.X - A.X, B.Y - A.Y);
                Point s = new Point(D.X - C.X, D.Y - C.Y);

                float CmPxr = CmP.X * r.Y - CmP.Y * r.X;
                float CmPxs = CmP.X * s.Y - CmP.Y * s.X;
                float rxs = r.X * s.Y - r.Y * s.X;

                if (CmPxr == 0f)
                {
                    // Lines are collinear, and so intersect if they have any overlap

                    return ((C.X - A.X < 0f) != (C.X - B.X < 0f))
                        || ((C.Y - A.Y < 0f) != (C.Y - B.Y < 0f));
                }

                if (rxs == 0f)
                    return false; // Lines are parallel.

                float rxsr = 1f / rxs;
                float t = CmPxs * rxsr;
                float u = CmPxr * rxsr;

                return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);



        }

        public Point FindIntersection(Line al, Line bl)
        {

            float x1, x2, x3, x4, y1, y2, y3, y4, miu, lambda;

            x1 = al.StartPoint.X;
            x2 = al.EndPoint.X;
            x3 = bl.StartPoint.X;
            x4 = bl.EndPoint.X;

            y1 = al.StartPoint.Y;
            y2 = al.EndPoint.Y;
            y3 = bl.StartPoint.Y;
            y4 = bl.EndPoint.Y;

            if (x2 == x3 && y2 == y3) // a.end == b.start
                return new Point(x2, y2);
            if (x4 == x1 && y4 == y1) // b.end == a.start
                return new Point(x4, y4);
            
            if (((x4 - x3) * (y1 - y2) - (x1 - x2) * (y4 - y3)) == 0) // denominator is 0 => colinearity
            {
                if (x2 == x3 && y2 == y3) // a.end == b.start
                    return new Point(x2, y2);
                if (x4 == x1 && y4 == y1) // b.end == a.start
                    return new Point(x4, y4);
            }

            miu = ((y3 - y4) * (x1 - x3) + (x4 - x3) * (y1 - y3) ) / ((x4 - x3) * (y1 - y2) - (x1 - x2) * (y4 - y3));
            lambda = ((y1 - y2) * (x1 - x3) + (x2 - x1) * (y1 - y3) ) / ((x4 - x3) * (y1 - y2) - (x1 - x2) * (y4 - y3));

            if (miu >= 0 && miu <= 1 && lambda >=0 && lambda <= 1)
            {
                Point result = new Point(x1 + miu * (x2 - x1), x3 + lambda * (x4 - x3));
                    Console.WriteLine("Points intersect in:" + result);
                return result;

            }

            return new Point(-10000, -10000);

        }


        public  bool DoLineSegmentsIntersect(Line a, Line b) {


            return (lineSegmentTouchesOrCrossesLine(a, b)
            && lineSegmentTouchesOrCrossesLine(b, a));
        }

    }




    class MainClass
    {
        public static void Main(string[] args)
        {
           
            System.IO.StreamReader file = new System.IO.StreamReader("/Users/alexandrurustin/Projects/SegmentsIntersection/SegmentsIntersection/points.txt");
            string line;
            Line[] lines = new Line[3];

            int i = 0;

            while ((line=file.ReadLine()) != null)
            {
                
                Console.WriteLine(line);
                string[] xy = line.Split(' ');
                lines[i] = new Line(float.Parse(xy[0]), float.Parse(xy[1]), float.Parse(xy[2]), float.Parse(xy[3]));
                i++;
                
            }

            IntersectionTest myinter = new IntersectionTest();

           
           
              
                Console.WriteLine (myinter.FindIntersection(lines[j],lines[(j+1) % lines.Length]));


        }
    }
}

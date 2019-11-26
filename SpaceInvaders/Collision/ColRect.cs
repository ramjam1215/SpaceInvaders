using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColRect : Azul.Rect
    {
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;

        public ColRect(float x, float y, float width, float height) 
            : base(x,y,width, height)
        {

        }

        public ColRect(Azul.Rect pRect) 
            : base(pRect)
        {

        }

        public ColRect(ColRect pRect)
            : base(pRect)
        {

        }

        public ColRect() 
            : base()
        {

        }

        static public bool Intersect(ColRect ColRect_A, ColRect ColRect_B)
        {
            bool status = false;

            float A_minX = ColRect_A.x - ColRect_A.width / 2;
            float A_maxX = ColRect_A.x + ColRect_A.width / 2;
            
            float A_minY = ColRect_A.y - ColRect_A.height / 2;
            float A_maxY = ColRect_A.y + ColRect_A.height / 2;

            float B_minX = ColRect_B.x - ColRect_B.width / 2;
            float B_maxX = ColRect_B.x + ColRect_B.width / 2;

            float B_minY = ColRect_B.y - ColRect_B.height / 2;
            float B_maxY = ColRect_B.y + ColRect_B.height / 2;

            if((B_maxX < A_minX) || (B_minX > A_maxX) || (B_maxY < A_minY) || (B_minY > A_maxY))
            {
                status = false;
            }

            else
            {
                status = true;
            }

            return status;
        }

        public void Union(ColRect ColRect)
        {
            

            if( (this.x - this.width / 2) < (ColRect.x - ColRect.width / 2) )
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }
                


            if( (this.x + this.width / 2) > (ColRect.x + ColRect.width / 2) )
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            //-------------------------------------------------------------------
            //
            //
            //--------------------------------------------------------------------

            if ( (this.y - this.height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }



            if ((this.y + this.height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            //dont switch these up
            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;

        }
    }
}

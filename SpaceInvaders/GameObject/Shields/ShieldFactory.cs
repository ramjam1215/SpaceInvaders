using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pColSpriteBatch;
        private Composite pTree;

        public ShieldFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name colSpriteBatchName, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pColSpriteBatch = SpriteBatchMan.Find(colSpriteBatchName);
            Debug.Assert(this.pColSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }

        ~ShieldFactory()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~ShieldFactory(): ");
#endif
            this.pSpriteBatch = null;
            this.pColSpriteBatch = null;
            this.pTree = null;
        }

        public void SetParent(GameObject pParentNode)
        {
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }

        public GameObject Create(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop1, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop2:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop2, posX, posY);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftBottom, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop1:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop1, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop2:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop2, posX, posY);
                    break;

                case ShieldCategory.Type.RightBottom:
                    pShield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightBottom, posX, posY);
                    break;


                case ShieldCategory.Type.Root:
                    pShield = new ShieldRoot(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    pShield = new ShieldGrid(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    break;

                case ShieldCategory.Type.Column:
                    pShield = new ShieldColumn(gameName, GameSprite.Name.NullObject, posX, posY);
                    pShield.SetCollisionColor(1.0f, 0.0f, 0.0f);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            this.pTree.Add(pShield);

            pShield.ActivateGameSprite(this.pSpriteBatch);
            pShield.ActivateCollisionSprite(this.pColSpriteBatch);

            return pShield;
        }

        //need to create the root node first before using
        public static void ShieldCreator(float startX, float startY, GameObject.Name shieldName)
        {
            Composite pShieldRoot = (Composite)GONodeMan.Find(GameObject.Name.ShieldRoot);
            Debug.Assert(pShieldRoot != null);

            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            GameObject pShieldGrid;

            SF.SetParent(pShieldRoot);
            pShieldGrid = SF.Create(ShieldCategory.Type.Grid, shieldName);

            //j represents the columns
            int j = 0;

            GameObject pShieldColumn;

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            float start_x = startX;
            float start_y = startY;
            float off_x = 0;
            float brickWidth = 10.0f;
            float brickHeight = 5.0f;


            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.LeftTop2, GameObject.Name.ShieldBrick, start_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            SF.SetParent(pShieldGrid);
            pShieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1 + j++);

            SF.SetParent(pShieldColumn);

            off_x += brickWidth;
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
            SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
            SF.Create(ShieldCategory.Type.RightTop2, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
            SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
        }
    }
}

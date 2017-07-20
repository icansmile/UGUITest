﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public abstract class UFControl
    {
        const int GuideNum = 8;
        const int GuideMargin = 3;
        const int GuideSize = (GuideMargin * 2) + 1;

        private Vector2 propertyViewScroll = Vector2.zero;

        private bool isHidden = false;

        private bool isEnabled = true;

        private bool foldout = true;

        [XmlIgnore]
        protected UFControl parent = null;

        [XmlIgnore]
        protected GUIContent name = new GUIContent("");

        [XmlIgnore]
        protected GUIContent text = new GUIContent("");

        [XmlIgnore]
        public Action<string> OnTextChanged = null;

        public List<UFControl> childList = new List<UFControl>();

        public Rect DrawRect = new Rect(0,0,100,100);

        public string Name
        {
            get
            {
                return this.name.text;
            }
            set
            {
                this.name.text = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text.text;
            }
            set
            {
                if(this.text.text != value)
                {
                    this.text.text = value;
                    if(this.OnTextChanged != null)
                    {
                        this.OnTextChanged(this.text.text);
                    }
                }
            }
        }

        public Rect DrawRectWithGuide
        {
            get
            {
                return new Rect(this.DrawRect.x - GuideSize, this.DrawRect.y - GuideSize, this.DrawRect.width + (GuideSize * 2), this.DrawRect.height + (GuideSize * 2));
            }
        }

        public Vector2 ParentPosition
        {
            get
            {
                if(this.parent != null)
                {
                    return this.parent.ParentPosition + this.parent.DrawRect.position;
                }
                return Vector2.zero;
            }
        }

        public Vector2 GrobalPosition
        {
            get
            {
                if(this.parent != null)
                {
                    return this.parent.GrobalPosition + this.DrawRect.position;
                }
                return this.DrawRect.position;
            }
        }

        public int TreeCount
        {
            get
            {
                int count = 1;
                ForTree(node => count += node.childList.Count);
                return count;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
            }
        }

        public bool IsHidden
        {
            get
            {
                return this.isHidden;
            }
            set
            {
                this.isHidden = value;
            }
        }

        public bool Foldout
        {
            get
            {
                return this.foldout;
            }
            set
            {
                this.foldout = value;
            }
        }

        public bool HasParent
        {
            get
            {
                return (this.parent != null);
            }
        }

        public bool HasChild
        {
            get
            {
                return this.childList.Count > 0;
            }
        }

        public int Nest
        {
            get
            {
                if(this.parent != null)
                {
                    return this.parent.Nest + 1;
                }
                return 0;
            }
        }

        public abstract string DefaultName { get; }

        public abstract Vector2 DefaultSize { get; }

        public virtual GUIStyle DesignGUIStyle { get { return EditorStyles.label; } }

        public UFControl()
        {
            Debug.Log("UFControl constructor");
            this.DrawRect = new Rect(Vector2.zero, DefaultSize);
        }

        public void Add(UFControl child)
        {
            child.parent = this;
            this.childList.Add(child);
        }

        public void RemoveFromTree()
        {
            if(this.parent != null)
            {
                this.parent.childList.Remove(this);
            }
        }

        public List<UFControl> GetOutlineDrawList()
        {
            List<UFControl> list = new List<UFControl>(this.TreeCount);
            GetOutlineDrawListInternal(list);
            return list;
        }

        public void GetOutlineDrawListInternal(List<UFControl> list)
        {
            list.Add(this);
            if(this.foldout)
            {
                this.childList.ForEach(child => child.GetOutlineDrawListInternal(list));
            }
        }

        public void ForTree(System.Action<UFControl> action)
        {
            if(action != null)
            {
                action(this);
                this.childList.ForEach(child => child.ForTree(action));
            }
        }

        public void ForTreeFromChild(System.Action<UFControl> action)
        {
            if(action != null)
            {
                for(int i = this.childList.Count - 1; i >= 0; --i)
                {
                    this.childList[i].ForTreeFromChild(action);
                }
                action(this);
            }
        }

        public abstract void Draw();

        public abstract void DrawByRect();

        public virtual void DrawDesign()
        {
            GUILayout.Label(this.Text, this.DesignGUIStyle);
        }

        public virtual void DrawDesignByRect()
        {
            GUI.Label(this.DrawRect, this.Text, this.DesignGUIStyle);
        }

        public virtual void RefleshHierarchy()
        {
            this.childList.ForEach(child =>
            {
                child.parent = this;
                child.RefleshHierarchy();
            });
        }

        public void DrawProperty()
        {
            this.propertyViewScroll = GUILayout.BeginScrollView(this.propertyViewScroll);
            {
                DrawPropertyItem("Name", () => Name = EditorGUILayout.TextField(Name));
                DrawPropertyItem("Text", () => Text = EditorGUILayout.TextField(Text));
                DrawPropertyItem("Enabled", () => IsEnabled = EditorGUILayout.Toggle(IsEnabled));
                this.DrawPropertyOriginal();
            }
            GUILayout.EndScrollView();
            if(GUI.changed)
            {
                UFStudio.RepaintAll();
            }
        }

        protected virtual void DrawPropertyOriginal()
        {

        }

        protected void DrawPropertyItem(string label, System.Action drawAction)
        {
            GUILayout.BeginHorizontal(EditorStyles.textArea);
            {
                GUILayout.Label(label, GUILayout.Width(150));
                drawAction();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(-3);
        }

        public void DrawGuide()
        {
            Rect prev = new Rect(this.GrobalPosition, this.DrawRect.size);
            Rect current = GUI.Window(-1, prev, DrawGuideRect, "", "grey_border");

            float sx = current.x;
            float cx = current.center.x;
            float ex = current.x + current.width;
            float sy = current.y;
            float cy = current.center.y;
            float ey = current.y + current.height;

            Rect[] guides = new Rect[GuideNum];
            guides[0].x = sx - GuideSize;
            guides[0].y = sy - GuideSize;
            guides[1].x = cx - GuideMargin;
            guides[1].y = sy - GuideSize;
            guides[2].x = ex;
            guides[2].y = sy - GuideSize;
            guides[3].x = ex;
            guides[3].y = cy - GuideMargin;
            guides[4].x = ex;
            guides[4].y = ey;
            guides[5].x = cx - GuideMargin;
            guides[5].y = ey;
            guides[6].x = sx - GuideSize;
            guides[6].y = ey;
            guides[7].x = sx - GuideSize;
            guides[7].y = cy - GuideMargin;
            guides[0].width = GuideSize;
            guides[0].height = GuideSize;
            guides[1].width = GuideSize;
            guides[1].height = GuideSize;
            guides[2].width = GuideSize;
            guides[2].height = GuideSize;
            guides[3].width = GuideSize;
            guides[3].height = GuideSize;
            guides[4].width = GuideSize;
            guides[4].height = GuideSize;
            guides[5].width = GuideSize;
            guides[5].height = GuideSize;
            guides[6].width = GuideSize;
            guides[6].height = GuideSize;
            guides[7].width = GuideSize;
            guides[7].height = GuideSize;

            for(int i = 0; i < GuideNum; ++i)
            {
                Rect guide = guides[i];
                Rect result = GUI.Window(i, guide, DrawGuideRect, "", "LODSliderRangeSelected");
                if(guide.center.x != result.center.x && guide.center.y != result.center.y)
                {
                    float rx = result.center.x;
                    float ry = result.center.y;
                    switch(i)
                    {
                        case 0:
                            current.width += sx - rx;
                            current.height += sy - ry;
                            current.x = rx;
                            current.y = ry;
                            break;
                        case 1:
                            current.height += sy - ry;
                            current.y = ry;
                            break;
                        case 2:
                            current.width += rx - ex;
                            current.height += sy - ry;
                            current.y = ry;
                            break;
                        case 3:
                            current.width += rx - ex;
                            break;
                        case 4:
                            current.width += rx - ex;
                            current.height += ry - ey;
                            break;
                        case 5:
                            current.height += ry - ey;
                            break;
                        case 6:
                            current.width += sx - rx;
                            current.height += ry - ey;
                            current.x = rx;
                            break;
                        case 7:
                            current.width += sx - rx;
                            current.x = rx;
                            break;
                    }
                }
            }
            Vector2 delta = current.position - prev.position;
            this.DrawRect = new Rect(this.DrawRect.position + delta, current.size);
        }

        private void DrawGuideRect(int id)
        {
            GUI.DragWindow();
        }

        public void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new " + this.GetType().Name + "();");
            builder.WriteLine("this." + this.Name + ".Text = \"" + this.Text + "\";");
            builder.WriteLine("this." + this.Name + ".Name = \"" + this.Name + "\";");
            builder.WriteLine("this." + this.Name + ".IsEnabled = " + (this.IsEnabled ? "true" : "false") + ";");
            builder.WriteLine("this." + this.Name + ".IsHidden = " + (this.IsHidden ? "true" : "false") + ";");
            builder.WriteLine("this." + this.Name + ".DrawRect = new Rect(" +
                this.DrawRect.x.ToString() + "f, " +
                this.DrawRect.y.ToString() + "f, " +
                this.DrawRect.width.ToString() + "f, " +
                this.DrawRect.height.ToString() + "f);");
            WriteCodeAdditional(builder);
        }

        public void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private " + this.GetType().Name + " " + this.Name + ";");
        }

        public virtual void WriteCodeAdditional(CodeBuilder builder)
        {

        }

        public void WriteNativeRectDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("public static readonly Rect {0} = new Rect({1}f, {2}f, {3}f, {4}f);",
                this.Name, this.DrawRect.x, this.DrawRect.y, this.DrawRect.width, this.DrawRect.height);
        }

        public virtual void WriteNativeContentDefinitionCode(CodeBuilder builder)
        {

        }

        public virtual void WriteNativeConstDefinitionCode(CodeBuilder builder)
        {

        }

        public virtual void WriteNativeVariableDefinitionCode(CodeBuilder builder)
        {

        }

        public abstract void WriteNativeCodeByRect(CodeBuilder builder);
    }
}
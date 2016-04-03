using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(TextMesh))]
[ExecuteInEditMode]
public abstract class TextBase : MonoBehaviour
{
	public abstract string GetText();

	public virtual float TextScale
	{
		get
		{
			return 1F;
		}
	}

	public virtual void Update()
	{
		var text = GetComponent<TextMesh>();

		if(text)
		{
			text.text = GetText();
		}
	}

	public virtual void Reset()
	{
		RefreshText();
	}

	public virtual void OnValidate()
	{
		RefreshText();
	}

	void RefreshText()
	{
		var text = GetComponent<TextMesh>();

		if(text)
		{
			text.anchor = TextAnchor.MiddleCenter;
			text.fontSize = 128;
			transform.localScale = new Vector3(0.02F * TextScale, 0.02F * TextScale, 1F);
		}
	}
}

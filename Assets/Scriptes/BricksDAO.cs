using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksDAO {
	private static BricksDAO instance = null;
	private int totalNumberOfBricks = 0;

	public BricksDAO() {
		if (instance == null) {
			instance = this;
		}
	}

	public void setTotalNumberOfBricks(int totalNumberOfBricks) {
		instance.totalNumberOfBricks = totalNumberOfBricks;
	}

	public int getTotalNumberOfBricks () {
		return instance.totalNumberOfBricks;
	}
}

<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.vote.query.totalCount"
        :pageSize="stores.vote.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
					<FormItem>
		<Select style="width: 180px;" v-model="stores.vote.query.kwsszdgz" placeholder="所属重点工作" :clearable="true" filterable>
			<Option v-for="item in prioritydata" :key="item.priorityUUID" :value="item.priorityUUID" :title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
		</Select>
            </FormItem>
                  <FormItem>
                    <Input
                      type="text"
                      search
					  style="width: 180px;"
                      :clearable="true"
                      v-model="stores.vote.query.kwbt"
                      placeholder="输入任务标题搜索..."
					  @on-search="handleSearchs()"
                    >
                    </Input>
                  </FormItem>
                  <FormItem>
					  <Select v-model="stores.vote.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人" filterable>
					  	<Option v-for="item in fuzerendata" :value="item.systemUserUUID" :key="item.systemUserUUID">{{item.realName}}</Option>
					  </Select>
                  </FormItem>
                  <!-- <FormItem>
              <Select v-model="stores.vote.query.zt" placeholder="状态" clearable>
                <Option value="0">进行中</Option>
				<Option value="1">审核中</Option>
				<Option value="2">已完成</Option>
              </Select>
            </FormItem> -->
				<FormItem>
              <Select v-model="stores.vote.query.yxj" placeholder="请选择优先级" clearable>
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
              </Select>
            </FormItem>
				  <FormItem>
				  <Button
				  v-can="'search'"
				    icon="md-search"
				    type="primary"
				    @click="handleSearchs()"
				    title="查询">查询
				  </Button>
				  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
				  v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增任务"
                >新增任务</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
            slot="table"
            ref="tables"
            :border="false"
            size="small"
            :highlight-row="true"
			:row-class-name="rowClassName"
            :data="stores.vote.data"
            :columns="stores.columns"
            @on-selection-change="handleSelectionChange"
            @on-refresh="handleRefresh"
            @on-page-change="handlePageChanged"
            @on-page-size-change="handlePageSizeChanged"
            @on-sort-change="handleSortChange"
          >
		  <template slot-scope="{row,index}" slot="auditStatus">
		    <span>{{getauditStatus(row.auditStatus)}}</span>
		  </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定将此任务放入回收站?"
              @on-ok="handleDelete(row)"
              >
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
			<Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
			  <Button
			  v-can="'show'"
			    type="success"
			    size="small"
			    shape="circle"
			    icon="md-search"
			    @click="lookEwq(row)"
			  ></Button>
			</Tooltip>
            <Tooltip placement="top" v-if="editshow(row.auditStatus)" content="编辑" :delay="1000" :transfer="true">
              <Button v-can="'edit'" type="primary" size="small" shape="circle" icon="md-create" @click="handleEdit(row)"></Button>
            </Tooltip>
        </template>
        </Table>
      </dz-table>
    </Card>
	
	
	<!-- 添加、修改的表单 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formtask" :rules="formModel.rules" label-position="top">
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="所属重点工作" prop="priorityUUID">
		<Select v-model="formModel.fields.priorityUUID" placeholder="所属重点工作" filterable>
			<Option v-for="item in prioritydata" :value="item.priorityUUID":title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
		</Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="任务标题" prop="missionHeadline">
              <Input v-model="formModel.fields.missionHeadline" placeholder="请输入任务标题"/>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="负责人" prop="selectfuzename">


<Input v-model="formModel.fields.selectfuzename" @on-change="selectfuze" style="width: 150px;" placeholder="请选择负责人" :readonly="true"/>
					<i-button type="info" @click="selectfuze(rowid)">选择负责人</i-button>

		<!-- <Select v-model="formModel.fields.principal" placeholder="负责人" filterable>
			<Option v-for="item in fuzerendata" :value="item.systemUserUUID">{{item.realName}}</Option>
		</Select> -->
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="优先级" prop="priority">
              <Select v-model="formModel.fields.priority">
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="开始时间" prop="startTime">
             <Date-picker type="datetime" format="yyyy-MM-dd HH:mm" :options="options3" placeholder="请选择时间" @on-change="formModel.fields.startTime=$event" :value="formModel.fields.startTime" ></Date-picker>
            </FormItem>
          </Col>
		  <Col span="12">
		    <FormItem label="结束时间" prop="finishTime">
		     <Date-picker type="datetime" format="yyyy-MM-dd HH:mm" :options="options3" placeholder="请选择时间" @on-change="formModel.fields.finishTime=$event" :value="formModel.fields.finishTime"></Date-picker>
		    </FormItem>
		  </Col>
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="任务描述" prop="missionDescribe">
              <Input
                v-model="formModel.fields.missionDescribe"
                placeholder="请输入任务描述"
                type="textarea"
                :rows="5"
              />
            </FormItem>
          </Col>
        </Row> 
        <Row :gutter="16">
           <!--<Col span="12">
            <FormItem label="计划工时" prop="manhour">
              <Input-number :max="999999" :min="1" v-model="formModel.fields.manhour"></Input-number> 天
            </FormItem>
          </Col>-->
			<Col span="12">
				<FormItem label="参与人" prop="selectcanyurenname">
					<Input v-model="formModel.fields.selectcanyurenname" @on-change="selectcanyu" style="width: 150px;" placeholder="请选择参与人" :readonly="true"/>
					<!-- <Button icon="md-checkmark-circle" type="ghost" @click="selectcanyu">选择参与人</Button> -->
					<i-button type="info" @click="selectcanyu(rowid)">选择参与人</i-button>
				</FormItem>
			</Col>
			<!-- <Col span="12">
				<FormItem label="审批人" prop="selectshenpiname">
					<Input v-model="formModel.fields.selectshenpiname" @on-change="selectshenpi" style="width: 150px;" placeholder="请选择审批人" :readonly="true"/>
					<i-button type="info" @click="selectshenpi(rowid)">选择审批人</i-button>
					<!-- <Select v-model="formModel.fields.approver" placeholder="审批人" filterable clearable>
						<Option v-for="item in approver" :value="item.systemUserUUID">{{item.realName}}</Option>
					</Select> -->
				<!-- </FormItem>
          	</Col> -->
			  <Col span="12">
		      <FormItem label="创建人" prop="establishName">
				  <Input v-model="formModel.fields.establishName" :readonly="true"  placeholder="创建人"/>
		      </FormItem>
		    </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmit">提 交</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
	
	<!-- 查看的表单 -->
	<Drawer
	  title="查看任务"
	  v-model="formModel2.opened"
	  width="600"
	  :mask-closable="false"
	  :mask="true"
	  :styles="styles"
	  >
	  <Form :model="formModel2.fields" ref="formtask2" :rules="formModel2.rules" label-position="top">
	    <Row :gutter="16">
	      <Col span="12">
	        <FormItem label="所属重点工作" prop="priorityname">
				<Input v-model="formModel2.fields.priorityname" :title="formModel2.fields.priorityname" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>
	      <Col span="12">
	        <FormItem label="任务标题" prop="missionHeadline">
				<Input v-model="formModel2.fields.missionHeadline" :title="formModel2.fields.missionHeadline" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>
	    </Row>
	    <Row :gutter="16">
	      <Col span="12">
	        <FormItem label="负责人" prop="selectfuzename">
			<Input v-model="formModel2.fields.selectfuzename" :title="formModel2.fields.selectfuzename" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>
	      <Col span="12">
	        <FormItem label="优先级" prop="priority">
		<Input v-model="formModel2.fields.priority" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>
	    </Row>
	    <Row :gutter="16">
	      <Col span="12">
	        <FormItem label="开始时间" prop="startTime">
				<Input v-model="formModel2.fields.startTime" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>
		  <Col span="12">
		    <FormItem label="结束时间" prop="finishTime">
		     <Input v-model="formModel2.fields.finishTime" :readonly="true" placeholder="未知..."/>
		    </FormItem>
		  </Col>
	    </Row>
	    <Row :gutter="16">
	      <Col span="50">
	        <FormItem label="任务描述" prop="missionDescribe">
	          <Input
	            v-model="formModel2.fields.missionDescribe"
	            placeholder="请输入任务描述"
	            type="textarea"
	            :rows="5"
				:readonly="true"
	          />
	        </FormItem>
	      </Col>
	    </Row> 
	    <Row :gutter="16">
	      <!-- <Col span="12">
	        <FormItem label="计划工时" prop="manhourt">
	          <Input v-model="formModel2.fields.manhourt" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>-->
			<Col span="12">
				<FormItem label="参与人" prop="selectcanyurenname">
					<Input v-model="formModel2.fields.selectcanyurenname" :title="formModel2.fields.selectcanyurenname" :readonly="true" placeholder="未知..."/>
				</FormItem>
			</Col>
			<!-- <Col span="12">
				<FormItem label="审批人" prop="selectshenpiname">
					<Input v-model="formModel2.fields.selectshenpiname"  :title="formModel2.fields.selectshenpiname" :readonly="true" placeholder="未知..."/>
				</FormItem>
	        </Col> -->
	    </Row>
	
	    <!-- <Row :gutter="16">
		  <Col span="12">
		      <FormItem label="状态" prop="auditStatus">
				  <Input v-model="formModel2.fields.auditStatus" :readonly="true" placeholder="未知..."/>
		      </FormItem>
		    </Col>
	    </Row> -->

		<Row :gutter="16">
		  <Col span="12">
		    <FormItem label="创建时间" prop="establishTime">
			<Input v-model="formModel2.fields.establishTime" :readonly="true" placeholder="未知..."/>
		    </FormItem>
		  </Col>
		  <Col span="12">
		      <FormItem label="创建人" prop="establishName">
				  <Input v-model="formModel2.fields.establishName" :readonly="true" placeholder="未知..."/>
		      </FormItem>
		    </Col>
		</Row>

	    <Row :gutter="16" v-if="beizhushow">
	      <Col span="50">
	        <FormItem label="备注" prop="remark">
	          <Input
	            v-model="formModel2.fields.remark"
	            placeholder="备注"
	            type="textarea"
	            :rows="5"
				:readonly="true"
	          />
	        </FormItem>
	      </Col>
	    </Row> 
	
	    <Row :gutter="16" v-if="yijianshow">
	      <Col span="50">
	        <FormItem label="审核意见" prop="auditOpinion">
	          <Input
	            v-model="formModel2.fields.auditOpinion"
	            placeholder="备注"
	            type="textarea"
	            :rows="5"
				:readonly="true"
	          />
	        </FormItem>
	      </Col>
	    </Row>
		<!-- 		<label style="font-size: 15px;">操作日志：</label>
				<div v-for="item in rizhidata">
					<Row :gutter="16" v-if="qitashow(item.establishName)">
						<label style="font-size: 12px;margin-right: 5px;">{{item.chuanjianname}}:</label>
						<Tag style="max-width: 400px;white-space:normal; height: auto;font-size: 15px;">{{item.content}}</Tag>
						<label>{{item.establishTime}}</label>
					</Row>
					<Row :gutter="16" style="text-align: right;" v-if="dangqianshow(item.establishName)">
						<label>{{item.establishTime}}</label>
					<Tag style="font-size: 15px;max-width: 400px;white-space:normal;height: auto; background-color:lightgreen;text-align: left;">{{item.content}}</Tag>
					<label style="font-size: 12px;margin-right: 5px; color:green">{{item.chuanjianname}}</label>
					</row>
				</div>			
					<Row :gutter="18" style="margin-top: 10px; width: 600px;height: 50px; background-color: white; position: fixed;bottom: 0px;">
						<label>请输入内容：</label>
						<Input v-model="formmodelrizhi.content" placeholder="请输入" style="width: 70%;"/>
						<Button icon="md-checkmark-circle" type="primary" @click="fasong">发送</Button>
						</Row> -->
	  </Form>
	</Drawer>



	    <!-- <Drawer
      title="选择审批人"
      v-model="formModelshenpi.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelshenpi.fields" ref="formselect" label-position="top">


					<FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddata(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>

            <FormItem label="审批人" prop="fuze">

            <Transfer
                :data="formModelshenpi.fields.shenpileft"
                :target-keys="formModelshenpi.fields.shenpiright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange3"
              ></Transfer>
            </FormItem>

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelshenpi.opened = false">确 定</Button>
      </div>
    </Drawer> -->






    <Drawer
      title="选择负责人"
      v-model="formModelfuze.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelfuze.fields" ref="formselect" label-position="top">





		<vxe-table
          ref="xTree"
          max-height="600"
          :loading="loading"
          :data="tableData"
          :tree-config="{children: 'children'}"
		  :checkbox-config="{labelField: 'name', highlight: false}"
          @checkbox-change="selectChangeEvent">
		  <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node>
          </vxe-table-column>
		  <vxe-table-column>
            <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
            </template> -->
          </vxe-table-column>
        </vxe-table>

						<Input v-model="this.formModel.fields.selectfuzename" :readonly="true" placeholder="点击上方列表选择人员" type="textarea" :rows="5" />

					<!-- <FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddata(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>
            <FormItem label="负责人" prop="fuze">
            <Transfer
                :data="formModelfuze.fields.fuzeleft"
                :target-keys="formModelfuze.fields.fuzeright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange2"
              ></Transfer>
            </FormItem> -->

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelfuze.opened = false">确 定</Button>
      </div>
    </Drawer>
	
    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">



		<vxe-table
          ref="xTree"
          max-height="600"
          :loading="loading"
          :data="tableData"
          :tree-config="{children: 'children'}"
		  :checkbox-config="{labelField: 'name', highlight: false}"
          @checkbox-change="selectChangeEventcanyu">
		  <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node>
          </vxe-table-column>
		  <vxe-table-column>
            <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
            </template> -->
          </vxe-table-column>
        </vxe-table>

						<Input v-model="this.formModel.fields.selectcanyurenname" :readonly="true" placeholder="点击上方列表选择人员" type="textarea" :rows="5" />


					<!-- <FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddataid(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>
            <FormItem label="参与人" prop="participant">
            <Transfer
                :data="formModelcanyu.fields.participantleft"
                :target-keys="formModelcanyu.fields.participantright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange"
              ></Transfer>
            </FormItem> -->

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
      </div>
    </Drawer>
	

  </div>
</template>
<style>
    .ivu-table .demo-table-info-row td{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-error-row td{
        /* background-color: #ff6600; */
        color: red;
    }
    .ivu-table td.demo-table-info-column{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-name {
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-age {
        background-color: #ff6600;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-address {
        background-color: #187;
        color: #fff;
    }
</style>
<script>

	import Vue from 'vue'
    import XEUtils from 'xe-utils'

import DzTable from "_c/tables/dz-table.vue";
import config from '@/config';
import {
  getdatalist,
} from "@/api/taskpai/wochuangjiantask";
import {
  getpersonaldiary,
  getfuzeren,
  getcanyuren,
  getcanyuren2,
  loaddata,
  baocunedit,
  binddataok,
  binddataok2,
  binddataok3,
  create,
  deletelist,
  caozuolist,
  appcreaterizhi,
  keshidata,
systemuserlistuuid,
systemuserlistid,
depadnuser,//树形控件加载数据 
} from "@/api/taskpai/taskapis";

export default {
  name: "rbac_user_page",
  components: {
    DzTable
  },
  data() {
    return {

				tableData: [],
				filterName: '',
				loading: false,
              originData: [],









      commands: {
        delete: { name: "delete", title: "删除" },
      },
	  options3: {
	                  disabledDate (date) {
	                      return date && date.valueOf() < Date.now() - 86400000;
	                  }
	              },
	  rowuuid:"",
	  //操作日志
	  formmodelrizhi:{
	  	missionUuid:"",//所属任务
	  	content:"",//日志内容
	  	establishName:"",//创建人
	  },
	  rizhidata:[],
	  panduanrowid:0,
	  yijianshow:false,//审核意见显示
	  beizhushow:false,//备注显示
	  prioritydata:[],//重点工作
	  fuzerendata:[],//负责人
	  approver:[],//审批人
	  rowid:"",//选择行的id
      formModel: {
        opened: false,
        title: "创建任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
		  principal: "",//负责人
		  selectfuzename:"",//选择的负责人姓名
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          //manhour:1,//计划工时
		  approver:"",//审批人
		  selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id	
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
		  auditStatus:"",//审核状态
        },
        rules: {
          missionHeadline: [{ type: "string", required: true, message: "请输入任务标题" }],
          selectfuzename: [{ type: "string", required: true, message: "请选择负责人" }],
		  priority:[{type: "string", required: true, message: "请选择优先级" }],
		  startTime:[{type: "string", required: true, message: "请选择开始时间" }],
		  finishTime:[{type: "string", required: true, message: "请选择结束时间" }],
		  selectcanyurenname:[{type: "string", required: true, message: "请选择参与人" }],
		  //approver:[{type: "string", required: true, message: "请选择审批人" }]
        }
      },
      formModel2: {
        opened: false,
        title: "查看任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
		  principal: "",//负责人uuid
		  selectfuzename:"",//负责人
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          //manhourt:'',//计划工时
		  approver:"",//审批人
		  selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
        }
      },
      formModel3: {
        opened: false,
        title: "完成任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
		  principal: "",//选择的负责人uuid
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          //manhourt:'',//计划工时
		  approver:"",//审批人
		  selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
		  remark:"",//备注
        }
      },
	formModelcanyu:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			participantleft:[],
			participantright:[],
		}
	},
		formModelfuze:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			fuzeleft:[],
			fuzeright:[],
		}
	},
			formModelshenpi:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			shenpileft:[],
			shenpiright:[],
		}
	},
					formMode4: {
					fields: {
						id:"",
						uuid:"",
						name:""
					}
				},
				keshidatalist:[],
	  
      stores: {
		  vote:{
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
			userguid:"",
			kwsszdgz:"",
            kwbt: "",
			kwfzr:"",
			zt:"",
			yxj:"",
			kwendtime:"",
            isDeleted: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          }, 
		  data: []
		  },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "任务标题", key: "missionHeadline",ellipsis: true},
            { title: "所属重点工作", key: "priorityHeadline",ellipsis: true},
            { title: "状态", key: "auditStatus",slot:"auditStatus"},
            { title: "负责人", key: "principal"},
            { title: "优先级", key: "priority"},

            { title: "开始时间",  ellipsis: true, tooltip: true, key: "startTime" },
            { title: "结束时间", key: "finishTime" },
            { title: "操作", align: "left", width: 150, className: "table-command-column",slot:"action" }
          ]
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建任务";
      }
      if (this.formModel.mode === "edit") {
        return "编辑任务";
      }
      return "";
    },
	selectedRowsId() {
	  return this.formModel.selection.map(x => x.id);
	}
  },
//树形控件数据绑定
			created () {
				this.shuxingonload();
          },



  methods: {
shuxingonload(){
			//this.tableData = window.MOCK_TREE_DATA_LIST
			depadnuser().then(res => {
					//this.tableData=res.data;

					setTimeout(() => {
              this.loading = false
              this.originData = res.data
              this.handleSearch()
            }, 300)					
				});
},


handleSearch () {
			  let filterName = XEUtils.toString(this.filterName).trim()
              if (filterName) {
                let options = { children: 'children' }
                let searchProps = ['name']
                this.tableData = XEUtils.searchTree(this.originData, item => searchProps.some(key => XEUtils.toString(item[key]).indexOf(filterName) > -1), options)
                // 搜索之后默认展开所有子节点
                this.$nextTick(() => {
                  this.$refs.xTree.setAllTreeExpand(true)
                })
              } else {
                this.tableData = this.originData
              }
            },
            // 创建一个防反跳策略函数，调用频率间隔 500 毫秒
            searchEvent: XEUtils.debounce(function () {
              this.handleSearch()
			}, 500, { leading: false, trailing: true }),
			

			//负责人选择
			selectChangeEvent ({ records }) {
				
			  var selename="";//姓名
			  var seleuuid="";//uuid
			  var seleid="";//id
			  for(let i=0;i<records.length;i++)
			  {
				  if(records[i].uuid)
				  {
					  selename+=records[i].name+",";
					  seleuuid+=records[i].uuid+",";
					  seleid+=records[i].id+",";
				  }
			  }

			  this.formModel.fields.selectfuzename=selename;
			  this.formModel.fields.principal=seleuuid;
			},
			
			//参与人选择
			selectChangeEventcanyu({ records }) {
				
			  var selename="";//姓名
			  var seleuuid="";//uuid
			  var seleid="";//id
			  for(let i=0;i<records.length;i++)
			  {
				  if(records[i].uuid)
				  {
					  selename+=records[i].name+",";
					  seleuuid+=records[i].uuid+",";
					  seleid+=records[i].id+",";
				  }
			  }

			  this.formModel.fields.selectcanyurenname=selename;
			  this.formModel.fields.participant=seleid;
			},









	  rowClassName (row, index) {
                if (row.auditStatus === "1"||row.auditStatus === "2") {
                    return 'demo-table-info-row';
                } else if (row.auditStatus === "3") {
                    return 'demo-table-error-row';
                }
                return '';
            },
      //获取调度数据
      loadVoteinitiateList() {
		this.stores.vote.query.userguid=this.$store.state.user.userGuid;//当前登录账号guid
		//获取当前账号负责的任务信息
        getdatalist(this.stores.vote.query).then(res => {
          this.stores.vote.data = res.data.data;
          this.stores.vote.query.totalCount = res.data.totalCount;
        });
	  },
	  //***************************************************************************
      handleSortChange(column) {
        this.stores.query.sort.direction = column.order;
        this.stores.query.sort.field = column.key;
        this.loadVoteinitiateList();
      },
      //刷新
      handleRefresh() {
        this.loadVoteinitiateList();
      },
	  //获取选择的行数据
      handleSelectionChange(selection) {
        this.formModel.selection = selection;
      },
	  //翻页
	  handlePageChanged(page) {
	    this.stores.vote.query.currentPage = page;
	    this.loadVoteinitiateList();
	  },
      //显示条数改变
      handlePageSizeChanged(pageSize) {
        this.stores.vote.query.pageSize = pageSize;
        this.loadVoteinitiateList();
      },
	  //搜索
	  handleSearchs(){
		  this.loadVoteinitiateList();
	  },
	  //点击添加打开窗口
	  handleShowCreateWindow(){
		  this.filterName="";
		this.shuxingonload();
		  this.formModel.mode="create";//添加
		  this.formModel.opened=true;//打开窗口
		  this.$refs["formtask"].resetFields();//清空填写的表单信息
		 this.formModelfuze.fields.fuzeright=[]//重新打开窗口，清空选择的人员信息负责人
		 this.formModelshenpi.fields.shenpiright=[]//重新打开窗口，清空选择的人员信息审批人
		 this.formModelcanyu.fields.participantright=[]//重新打开窗口，清空选择的人员信息 参与人
		  this.formModel.fields.establishName=this.$store.state.user.userName;//创建人
	  },
	  //点击编辑打开窗口
	  handleEdit(row){
		  this.filterName="";
		this.shuxingonload();
		 this.formModel.mode="edit";//修改
		 this.formModel.opened=true;
		 this.rowid=row.id;//保存选择行的id
		 this.formModelfuze.fields.fuzeright=[]//重新打开窗口，清空选择的人员信息 负责人
		 this.formModelshenpi.fields.shenpiright=[]//重新打开窗口，清空选择的人员信息审批人
		 this.formModelcanyu.fields.participantright=[]//重新打开窗口，清空选择的人员信息 参与人
		 loaddata({ id: row.id}).then(res=>{
			 this.formModel.fields=res.data.data
		 });
	  },
	  //重点工作下拉框
	  loadpersonaldiary(){
		  getpersonaldiary().then(res => {
			this.prioritydata = res.data.data
/* 			console.log("重点工作数据")
			console.log(this.prioritydata) */
		});
	  },
	  //负责人和审批人下拉框
	  loadfuzeren(){
		  getfuzeren().then(res => {
			this.approver=this.fuzerendata = res.data.data
		});
	  },
	  //穿梭框负责人 审批人
	  loaselect2(){
		  getcanyuren2().then(res=>{
		  	this.formModelshenpi.fields.shenpileft=this.formModelfuze.fields.fuzeleft=res.data.data
		  });
	  },
	   //穿梭框参与人
	  loaselect(){
		  getcanyuren().then(res=>{
		  			 this.formModelcanyu.fields.participantleft=res.data.data
		  });
	  },

			//选择科室重新绑定人员信息
			binddata(data){

				systemuserlistuuid({uuid:data}).then(res => {
					this.formModelshenpi.fields.shenpileft=this.formModelfuze.fields.fuzeleft=res.data.data
				})
				
			},
			binddataid(data){
				systemuserlistid({uuid:data}).then(res => {
					console.log(res)
					this.formModelcanyu.fields.participantleft=res.data.data
				})
			},
			keshijiazai(){
				keshidata().then(res => {
					this.keshidatalist=this.keshidatalistid=res.data.data;
					
				})
			},

//打开选择审批人窗口
selectshenpi(rowid){

		  this.formModelshenpi.opened=true
		  //修改
		  if(rowid!="")
		  {
			  console.log(rowid)
		binddataok3({ id: rowid}).then(res => {
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelshenpi.fields.shenpiright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelshenpi.fields.shenpiright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelshenpi.fields.shenpiright = this.formModelshenpi.fields.shenpiright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }
},
	  //选择审批人
	  handleSchoolChange3(newTargetKeys, direction, moveKeys){
		  this.formModelshenpi.fields.shenpiright = newTargetKeys;
		  this.formModel.fields.selectshenpiname=this.formModel.fields.approver="";//每次选择则负责人都重新赋值
	    for(let i=0;i<this.formModelshenpi.fields.shenpiright.length;i++)
	    {
	        for(let j=0;j<this.formModelshenpi.fields.shenpileft.length;j++)
	        {
	            if(this.formModelshenpi.fields.shenpiright[i]===this.formModelshenpi.fields.shenpileft[j].key)
	            {
	                this.formModel.fields.selectshenpiname  += this.formModelshenpi.fields.shenpileft[j].label+",";
	                this.formModel.fields.approver += this.formModelshenpi.fields.shenpileft[j].key+",";
	            }
	        }
	    }

	  },







//打开选择负责人窗口
selectfuze(rowid){
			this.filterName="";
		  this.formModelfuze.opened=true
		  //修改
		  if(rowid!="")
		  {
			  console.log(rowid)
		binddataok2({ id: rowid}).then(res => {
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelfuze.fields.fuzeright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelfuze.fields.fuzeright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelfuze.fields.fuzeright = this.formModelfuze.fields.fuzeright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }
},
	  //选择负责人
	  handleSchoolChange2(newTargetKeys, direction, moveKeys){
		  this.formModelfuze.fields.fuzeright = newTargetKeys;
		  this.formModel.fields.selectfuzename=this.formModel.fields.principal="";//每次选择则负责人都重新赋值
	    for(let i=0;i<this.formModelfuze.fields.fuzeright.length;i++)
	    {
	        for(let j=0;j<this.formModelfuze.fields.fuzeleft.length;j++)
	        {
	            if(this.formModelfuze.fields.fuzeright[i]===this.formModelfuze.fields.fuzeleft[j].key)
	            {
	                this.formModel.fields.selectfuzename  += this.formModelfuze.fields.fuzeleft[j].label+",";
	                this.formModel.fields.principal += this.formModelfuze.fields.fuzeleft[j].key+",";
	            }
	        }
	    }

	  },


	  //打开选择参与人窗口
	  selectcanyu(rowid){
		  this.filterName="";
		  this.formModelcanyu.opened=true
		  //修改
		  if(rowid!="")
		  {
		binddataok({ id: rowid}).then(res => {
			
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelcanyu.fields.participantright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelcanyu.fields.participantright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelcanyu.fields.participantright = this.formModelcanyu.fields.participantright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }

	  },
	  //选择参与人
	  handleSchoolChange(newTargetKeys, direction, moveKeys){
		  this.formModelcanyu.fields.participantright = newTargetKeys;
		  this.formModel.fields.selectcanyurenname=this.formModel.fields.participant="";//每次选择则参与人都重新赋值
	    for(let i=0;i<this.formModelcanyu.fields.participantright.length;i++)
	    {
	        for(let j=0;j<this.formModelcanyu.fields.participantleft.length;j++)
	        {
	            if(this.formModelcanyu.fields.participantright[i]===this.formModelcanyu.fields.participantleft[j].key)
	            {
	                this.formModel.fields.selectcanyurenname  += this.formModelcanyu.fields.participantleft[j].label+",";
	                this.formModel.fields.participant += this.formModelcanyu.fields.participantleft[j].key+",";
	            }
	        }
	    }
/* 		  console.log("选择的人员")
		  console.log(this.selectcanyurenname) */
	  },
	  //点击保存
	  handleSubmit(){
		  
		  let valid = this.validateForm();
		  if(valid)
		  {
		  if (this.formModel.mode === "create") {//保存添加的内容
		  this.formModel.fields.establisthtruename=this.$store.state.user.userGuid;//当前登录账号guid(数据库保存创建人)
			  create(this.formModel.fields).then(res => {
			    if (res.data.code === 200) {
			      this.$Message.success(res.data.message);
				  this.formModel.opened=false;
				  this.loadVoteinitiateList();
			    } else {
			      this.$Message.warning(res.data.message);
			    }
			  });
		  }
		  if (this.formModel.mode === "edit") {//保存修改的内容

			  baocunedit(this.formModel.fields).then(res => {
			    if (res.data.code === 200) {
			      this.$Message.success(res.data.message);
				  this.formModel.opened=false;
				  this.loadVoteinitiateList();
			    } else {
			      this.$Message.warning(res.data.message);
			    }
			  });
		  }	
						
		  }
		  

	  },
	  //删除多条数据
	    handleBatchCommand(command) {
	    if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
	      this.$Message.warning("请选择至少一条数据");
	      return;
	    }
	    this.$Modal.confirm({
	      title: "操作提示",
	      content:
	        "<p>确定要将所选任务放入回收站吗?</p>",
	      loading: true,
	      onOk: () => {
/* 			  console.log("删除选择的")
	  		console.log(this.selectedRowsId.join(",")) */
	       deletelist({ ids: this.selectedRowsId.join(",")}).then(res => {
	         if (res.data.code === 200) {
	           this.$Message.success(res.data.message);
	  			 this.loadVoteinitiateList();
	  			 this.$Modal.remove();//关闭提示框
	         } else {
	           this.$Message.warning(res.data.message);
	         }
	       });
	   
	      }
	    });
	  },
	  //删除单条数据
    handleDelete(row) {
      /* console.log(row.id) */
	  deletelist({ ids: row.id}).then(res => {
	    if (res.data.code === 200) {
	      this.$Message.success(res.data.message);
	  		this.loadVoteinitiateList();		  
		  $Message.success("操作成功");

	    } else {
	      this.$Message.warning(res.data.message);
	    }
	  });
    },
	  //查看
	  lookEwq(row){
		  this.yijianshow=this.beizhushow=false;
		  this.formModel2.opened=true;
		 loaddata({ id: row.id}).then(res=>{
			 console.log("返回的数据")
			 console.log(res)
			 this.formModel2.fields=res.data.data
			 if(row.auditStatus!="0")//已提交完成则显示备注
			 {
				 this.beizhushow=true;
			 }
			 if(res.data.data.auditOpinion!="" &&res.data.data.auditOpinion!=null)//显示审核信息
			 {
				this.yijianshow=true;
			 }
		 });
		 
		 this.rowuuid=row.missionUuid;
		 //获取操作日志

			setInterval(this.huoqu,500)//每隔500毫秒获取操作日志
		 
	  },
	  //获取操作日志
/* 	  	huoqu(){
			caozuolist({uuid:this.rowuuid}).then(res=>{
						 this.rizhidata=res.data.data
			})
		}, */
	  		
	  qitashow(e)
	  {
		  if(e==this.$store.state.user.userGuid)
		  {
			  return false;
		  }else
		  {
			  return true;
		  }
	  },
	  dangqianshow(e)
	  {
		  
		  if(e==this.$store.state.user.userGuid)
		  {
			  return true;
		  }else
		  {
			  return false;
		  } 
	  },
	  //发送操作日志
	  fasong(){
		  this.formmodelrizhi.missionUuid=this.formModel2.fields.missionUUID;
		  this.formmodelrizhi.establishName=this.$store.state.user.userGuid;
		  appcreaterizhi(this.formmodelrizhi).then(res=>{
			  if (res.data.code === 200) {
				  this.formmodelrizhi.content="";
			    this.$Message.success(res.data.message);		  
			    $Message.success("操作成功");
			  
			  console.log(this.formmodelrizhi.content)
			  } else {
			    this.$Message.warning(res.data.message);
			  }
		  })

	  },
	  
	  //验证表单信息
			validateForm() {
			  let _valid = false;
			  this.$refs["formtask"].validate(valid => {
			    if (!valid) {
			      this.$Message.error("请完善表单信息");
			    } else {
			      _valid = true;
			    }
			  });
			  return _valid;
			},
			//修改按钮显示与隐藏
			editshow(auditStatus){
				var show=false;
				if(auditStatus=="0"||auditStatus=="3")//进行中的任务可以修改
				{
					show=true;
				}
				return show;
			},
	  getauditStatus(auditStatus){
		  var auditStatusText = "未知";
		  switch (auditStatus) {
		    case "0":
		      auditStatusText = "进行中";
		      break;
		    case "1":
		      auditStatusText = "进行中";
		      break;
		    case "2":
		      auditStatusText = "已完成";
			  break;
			  case "3":
		      auditStatusText = "逾期";
		      break;
		  }
		  return auditStatusText;
	  }
	 },
  mounted() {
	  this.loadVoteinitiateList();//加载所有数据
	  this.loadpersonaldiary();//重点工作下拉框
	  this.keshijiazai();//科室下拉框
	  this.loadfuzeren();//审批人下拉框
	  this.loaselect();//穿梭框参与人
	  this.loaselect2();//穿梭框负责人
  }
}
</script>

<style>
</style>

